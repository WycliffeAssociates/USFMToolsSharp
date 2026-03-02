using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    /// <summary>
    /// Parses a USFM file into a Abstract Syntax Tree
    /// </summary>
    public partial class USFMParser
    {
        private readonly HashSet<string> IgnoredMarkers;
        private readonly bool IgnoreUnknownMarkers;
        private readonly bool hasIgnoredMarkers;

        private List<FrozenDictionary<Type, HierarchyDefinition>> HierarchyDefinitions { get; set; } = new();
        public event EventHandler<List<Marker>>? OnMarkersTokenized;

        public USFMParser(List<string>? tagsToIgnore = null, bool ignoreUnknownMarkers = false, List<Dictionary<Type, HierarchyDefinition>>? hierarchyDefinitions = null)
        {
            if (hierarchyDefinitions == null)
            {
                HierarchyDefinitions = [
                    DefaultHierarchies.Default.ToFrozenDictionary(),
                    DefaultHierarchies.Presentation.ToFrozenDictionary(),
                    DefaultHierarchies.Structure.ToFrozenDictionary()
                ];
            }
            hasIgnoredMarkers = tagsToIgnore != null && tagsToIgnore.Count != 0;
            IgnoredMarkers = hasIgnoredMarkers ? [..tagsToIgnore!]: [];
            IgnoreUnknownMarkers = ignoreUnknownMarkers;
        }

        /// <summary>
        /// Parses a string into a USFMDocument
        /// </summary>
        /// <param name="input">A USFM string</param>
        /// <returns>A USFMDocument representing the input</returns>
        public USFMDocument ParseFromString(string input)
        {
            var output = new USFMDocument();
            for (var i = 0; i < HierarchyDefinitions.Count; i++)
            {
                output.Hierarchies.Add(new HierarchyNode(output));
            }
            var markers = TokenizeFromString(input);
            
            // Clean out extra whitespace where it isn't needed
            markers = CleanWhitespace(markers);

            OnMarkersTokenized?.Invoke(this, markers);

            for (var markerIndex = 0; markerIndex < markers.Count; markerIndex++)
            {
                var marker = markers[markerIndex];
                if (marker is TRMarker && !output.GetTypesPathToLastMarker().Contains(typeof(TableBlock)))
                {
                    output.Insert(new TableBlock(), HierarchyDefinitions);
                }

                if(marker is QMarker parsedMarker && markerIndex != markers.Count - 1 && markers[markerIndex + 1] is VMarker)
                {
                    parsedMarker.IsPoetryBlock = true;
                }
                
                output.Insert(marker, HierarchyDefinitions);
            }

            output.NumberOfTotalMarkersAtParse = markers.Count;

            return output;
        }

        /// <summary>
        /// Removes all the unnecessary whitespace while preserving space between closing markers and opening markers
        /// </summary>
        /// <param name="input"></param>
        private static List<Marker> CleanWhitespace(List<Marker> input)
        {
            // We're guessing that the majority of this isn't whitespace so start the output at the size of the input to prevent resizing
            var output = new List<Marker>(input.Count);
            for(var index = 0; index < input.Count; index++)
            {
                if (! (input[index] is TextBlock block && string.IsNullOrWhiteSpace(block.Text)))
                {
                    output.Add(input[index]);
                    continue;
                }

                // If this is an empty text block at the beginning remove it
                if(index == 0)
                {
                    continue;
                }
                
                // If this is an empty text block at the end then remove it
                if(index == input.Count - 1)
                {
                    continue;
                }

                // If this isn't between and end marker and another marker then delete it
                if(!(input[index - 1].Identifier.EndsWith('*') && !input[index + 1].Identifier.EndsWith('*')))
                {
                    continue;
                }

                output.Add(input[index]);
            }
            return output;
        }

        /// <summary>
        /// Generate a list of Markers from a string
        /// </summary>
        /// <param name="input">USFM String to tokenize</param>
        /// <returns>A List of Markers based upon the string</returns>
        private List<Marker> TokenizeFromString(ReadOnlySpan<char> input)
        {
            var output = new List<Marker>(input.Count('\\'));
            var index = 0;
            var startOfMarker = 0;
            var endOfMarker = 0;
            var startOfContent = 0;
            var endOfContent = 0;
            var inMarker = false;
            var inContent = false;
            while (index < input.Length)
            {
                // If this is a backslash then we're starting a marker.
                if (input[index] == '\\')
                {
                    if (!inMarker && !inContent)
                    {
                        inMarker = true;
                        startOfMarker = index + 1;
                        index++;
                        continue;
                    }

                    if (inMarker)
                    {
                        // If the backslash is immediately after the start of the marker then this is an escaped backslash and not a marker
                        if (index == startOfMarker)
                        {
                            // Handle escaped backslash
                            inMarker = false;
                            endOfMarker = index + 1;
                            inContent = true;
                            startOfContent = index + 1;
                            index+=2;
                            continue;
                        }
                        endOfMarker = index;
                        // Handle marker without content
                        AddMarkerToList(input[startOfMarker ..endOfMarker], ReadOnlySpan<char>.Empty, startOfMarker, output);
                        startOfMarker = index +1;
                        index++;
                        continue;
                    }
                    
                    if (inContent)
                    {
                        inContent = false;
                        endOfContent = index;
                        
                        // Handle content here
                        AddMarkerToList(input[startOfMarker ..endOfMarker], startOfContent == endOfContent ? ReadOnlySpan<char>.Empty : input[startOfContent..endOfContent], startOfMarker, output);
                        inMarker = true;
                        startOfMarker = index + 1;
                        
                        index++;
                        continue;
                    }
                }

                if (inMarker && !IsValidMarkerCharacter(input[index]))
                {
                    endOfMarker = index;
                    bool isEndMarker = input[index] == '*';
                    bool markerEndsWithNumber = char.IsNumber(input[index]);
                    inMarker = false;
                    // If it's an end marker, skip the '*'
                    if (isEndMarker || markerEndsWithNumber)
                    {
                        endOfMarker++;
                        index++;
                    }
                    startOfContent = index;
                    inContent = true;
                    continue;
                }
                
                index++;
            }
            if (inMarker)
            {
                endOfMarker = index;
                // Handle marker without content
                AddMarkerToList(input[startOfMarker ..endOfMarker], ReadOnlySpan<char>.Empty, startOfMarker, output);
            }
            else if (inContent)
            {
                endOfContent = index;
                // Handle marker with content
                AddMarkerToList(input[startOfMarker ..endOfMarker], startOfContent == endOfContent ? ReadOnlySpan<char>.Empty : input[startOfContent..endOfContent], startOfMarker, output);
            }

            return output;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsValidMarkerCharacter(char c)
        {
            return char.IsLetter(c) || c == '-' || c == '+';
        }

        private void AddMarkerToList(ReadOnlySpan<char> marker, ReadOnlySpan<char> content, int index, List<Marker> output)
        {
            var trimmedMarker = marker.Trim();
            
            // Strip leading '+' for nested markers (older USFM style)
            // According to https://docs.usfm.bible/usfm/3.1.1/char/nesting.html
            if (trimmedMarker.Length > 0 && trimmedMarker[0] == '+')
            {
                trimmedMarker = trimmedMarker[1..];
            }
            
            var lookup = IgnoredMarkers.GetAlternateLookup<ReadOnlySpan<char>>();
            if (hasIgnoredMarkers && lookup.Contains(trimmedMarker))
            {
                if (!content.IsEmpty)
                {
                    output.Add(new TextBlock(content.TrimStart(' ').ToString()));
                }
                return;
            }
            var result = ConvertToMarker(trimmedMarker, content);
            result.marker.Position = index;

            // If this is an unknown marker and we're in Ignore Unknown Marker mode then don't add the marker. We still keep any remaining text though
            if (result.marker is UnknownMarker unknownMarker)
            {
                if (IgnoreUnknownMarkers)
                {
                    output.Add(new TextBlock(unknownMarker.ParsedValue));
                }
                else
                {
                    output.Add(result.marker);
                }
            }
            else
            {
                output.Add(result.marker);
            }

            if (!result.remainingText.IsEmpty)
            {
                output.Add(new TextBlock(result.remainingText.ToString()));
            }
        }
        private ConvertToMarkerResult ConvertToMarker(ReadOnlySpan<char> identifier, ReadOnlySpan<char> value)
        {
            Marker output = SelectMarker(identifier);
            var tmp = output.PreProcess(value);
            return new ConvertToMarkerResult(output, tmp);
        }

        private static readonly Dictionary<string, Func<ReadOnlySpan<char>, Marker>> MarkerFactories = new(StringComparer.Ordinal)
        {
            { "id", _ => new IDMarker() },
            { "ide", _ => new IDEMarker() },
            { "sts", _ => new STSMarker() },
            { "h", _ => new HMarker() },
            { "toc1", _ => new TOC1Marker() },
            { "toc2", _ => new TOC2Marker() },
            { "toc3", _ => new TOC3Marker() },
            { "toca1", _ => new TOCA1Marker() },
            { "toca2", _ => new TOCA2Marker() },
            { "toca3", _ => new TOCA3Marker() },

            /* Introduction Markers*/
            { "imt", _ => new IMTMarker() },
            { "imt1", _ => new IMTMarker() },
            { "imt2", _ => new IMTMarker() { Weight = 2 } },
            { "imt3", _ => new IMTMarker() { Weight = 3 } },
            { "is", _ => new ISMarker() },
            { "is1", _ => new ISMarker() },
            { "is2", _ => new ISMarker() { Weight = 2 } },
            { "is3", _ => new ISMarker() { Weight = 3 } },
            { "ib", _ => new IBMarker() },
            { "iq", _ => new IQMarker() },
            { "iq1", _ => new IQMarker() },
            { "iq2", _ => new IQMarker() { Depth = 2 } },
            { "iq3", _ => new IQMarker() { Depth = 3 } },
            { "iot", _ => new IOTMarker() },
            { "io", _ => new IOMarker() },
            { "io1", _ => new IOMarker() },
            { "io2", _ => new IOMarker() { Depth = 2 } },
            { "io3", _ => new IOMarker() { Depth = 3 } },
            { "ior", _ => new IORMarker() },
            { "ior*", _ => new IOREndMarker() },
            { "ili", _ => new ILIMarker() },
            { "ili1", _ => new ILIMarker() },
            { "ili2", _ => new ILIMarker() { Depth = 2 } },
            { "ili3", _ => new ILIMarker() { Depth = 3 } },
            { "ip", _ => new IPMarker() },
            { "ipi", _ => new IPIMarker() },
            { "im", _ => new IMMarker() },
            { "imi", _ => new IMIMarker() },
            { "ipq", _ => new IPQMarker() },
            { "imq", _ => new IMQMarker() },
            { "ipr", _ => new IPRMarker() },
            { "mt", _ => new MTMarker() },
            { "mt1", _ => new MTMarker() },
            { "mt2", _ => new MTMarker() { Weight = 2 } },
            { "mt3", _ => new MTMarker() { Weight = 3 } },
            { "c", _ => new CMarker() },
            { "cp", _ => new CPMarker() },
            { "ca", _ => new CAMarker() },
            { "ca*", _ => new CAEndMarker() },
            { "p", _ => new PMarker() },
            { "v", _ => new VMarker() },
            { "va", _ => new VAMarker() },
            { "va*", _ => new VAEndMarker() },
            { "vp", _ => new VPMarker() },
            { "vp*", _ => new VPEndMarker() },
            { "q", _ => new QMarker() },
            { "q1", _ => new QMarker() },
            { "q2", _ => new QMarker() { Depth = 2 } },
            { "q3", _ => new QMarker() { Depth = 3 } },
            { "q4", _ => new QMarker() { Depth = 4 } },
            { "qr", _ => new QRMarker() },
            { "qc", _ => new QCMarker() },
            { "qd", _ => new QDMarker() },
            { "qac", _ => new QACMarker() },
            { "qac*", _ => new QACEndMarker() },
            { "qm", _ => new QMMarker() { Depth = 1 } },
            { "qm1", _ => new QMMarker() { Depth = 1 } },
            { "qm2", _ => new QMMarker() { Depth = 2 } },
            { "qm3", _ => new QMMarker() { Depth = 3 } },

            { "m", _ => new MMarker() },
            { "d", _ => new DMarker() },
            { "ms", _ => new MSMarker() },
            { "ms1", _ => new MSMarker() },
            { "ms2", _ => new MSMarker() { Weight = 2 } },
            { "ms3", _ => new MSMarker() { Weight = 3 } },
            { "mr", _ => new MRMarker() },
            { "cl", _ => new CLMarker() },
            { "qs", _ => new QSMarker() },
            { "qs*", _ => new QSEndMarker() },
            { "f", _ => new FMarker() },
            { "fp", _ => new FPMarker() },
            { "fl", _ => new FLMarker() },
            { "qa", _ => new QAMarker() },
            { "nb", _ => new NBMarker() },
            { "fqa", _ => new FQAMarker() },
            { "fqa*", _ => new FQAEndMarker() },
            { "fq", _ => new FQMarker() },
            { "fq*", _ => new FQEndMarker() },
            { "pi", _ => new PIMarker() },
            { "pi1", _ => new PIMarker() },
            { "pi2", _ => new PIMarker() { Depth = 2 } },
            { "pi3", _ => new PIMarker() { Depth = 3 } },
            { "sp", _ => new SPMarker() },
            { "ft", _ => new FTMarker() },
            { "fr", _ => new FRMarker() },
            { "fr*", _ => new FREndMarker() },
            { "fk", _ => new FKMarker() },
            { "fv", _ => new FVMarker() },
            { "fv*", _ => new FVEndMarker() },
            { "f*", _ => new FEndMarker() },
            { "bd", _ => new BDMarker() },
            { "bd*", _ => new BDEndMarker() },
            { "it", _ => new ITMarker() },
            { "it*", _ => new ITEndMarker() },
            { "rem", _ => new REMMarker() },
            { "b", _ => new BMarker() },
            { "s", _ => new SMarker() },
            { "s1", _ => new SMarker() },
            { "s2", _ => new SMarker() { Weight = 2 } },
            { "s3", _ => new SMarker() { Weight = 3 } },
            { "s4", _ => new SMarker() { Weight = 4 } },
            { "s5", _ => new SMarker() { Weight = 5 } },
            { "bk", _ => new BKMarker() },
            { "bk*", _ => new BKEndMarker() },
            { "li", _ => new LIMarker() },
            { "li1", _ => new LIMarker() },
            { "li2", _ => new LIMarker() { Depth = 2 } },
            { "li3", _ => new LIMarker() { Depth = 3 } },
            { "add", _ => new ADDMarker() },
            { "add*", _ => new ADDEndMarker() },
            { "tl", _ => new TLMarker() },
            { "tl*", _ => new TLEndMarker() },
            { "mi", _ => new MIMarker() },
            { "sc", _ => new SCMarker() },
            { "sc*", _ => new SCEndMarker() },
            { "r", _ => new RMarker() },
            { "rq", _ => new RQMarker() },
            { "rq*", _ => new RQEndMarker() },
            { "w", _ => new WMarker() },
            { "w*", _ => new WEndMarker() },
            { "x", _ => new XMarker() },
            { "x*", _ => new XEndMarker() },
            { "xo", _ => new XOMarker() },
            { "xt", _ => new XTMarker() },
            { "xq", _ => new XQMarker() },
            { "pc", _ => new PCMarker() },
            { "cls", _ => new CLSMarker() },
            { "tr", _ => new TRMarker() },
            { "th1", _ => new THMarker() },
            { "thr1", _ => new THRMarker() },
            { "th2", _ => new THMarker() { ColumnPosition = 2 } },
            { "thr2", _ => new THRMarker() { ColumnPosition = 2 } },
            { "th3", _ => new THMarker() { ColumnPosition = 3 } },
            { "thr3", _ => new THRMarker() { ColumnPosition = 3 } },
            { "tc1", _ => new TCMarker() },
            { "tcr1", _ => new TCRMarker() },
            { "tc2", _ => new TCMarker() { ColumnPosition = 2 } },
            { "tcr2", _ => new TCRMarker() { ColumnPosition = 2 } },
            { "tc3", _ => new TCMarker() { ColumnPosition = 3 } },
            { "tcr3", _ => new TCRMarker() { ColumnPosition = 3 } },
            { "usfm", _ => new USFMMarker() },
            /* Character Styles */
            { "em", _ => new EMMarker() },
            { "em*", _ => new EMEndMarker() },
            { "bdit", _ => new BDITMarker() },
            { "bdit*", _ => new BDITEndMarker() },
            { "no", _ => new NOMarker() },
            { "no*", _ => new NOEndMarker() },
            { "k", _ => new KMarker() },
            { "k*", _ => new KEndMarker() },
            { "lf", _ => new LFMarker() },
            { "lik", _ => new LIKMarker() },
            { "lik*", _ => new LIKEndMarker() },
            { "litl", _ => new LITLMarker() },
            { "litl*", _ => new LITLEndMarker() },
            { "liv", _ => new LIVMarker() },
            { "liv*", _ => new LIVEndMarker() },
            { "ord", _ => new ORDMarker() },
            { "ord*", _ => new ORDEndMarker() },
            { "pm", _ => new PMMarker() },
            { "pmc", _ => new PMCMarker() },
            { "pmo", _ => new PMOMarker() },
            { "pmr", _ => new PMRMarker() },
            { "png", _ => new PNGMarker() },
            { "png*", _ => new PNGEndMarker() },
            { "pr", _ => new PRMarker() },
            { "qt", _ => new QTMarker() },
            { "qt*", _ => new QTEndMarker() },
            { "rb", _ => new RBMarker() },
            { "rb*", _ => new RBEndMarker() },
            { "sig", _ => new SIGMarker() },
            { "sig*", _ => new SIGEndMarker() },
            { "sls", _ => new SLSMarker() },
            { "sls*", _ => new SLSEndMarker() },
            { "wa", _ => new WAMarker() },
            { "wa*", _ => new WAEndMarker() },
            { "wg", _ => new WGMarker() },
            { "wg*", _ => new WGEndMarker() },
            { "wh", _ => new WHMarker() },
            { "wh*", _ => new WHEndMarker() },
            { "wj", _ => new WJMarker() },
            { "wj*", _ => new WJEndMarker() },
            { "nd", _ => new NDMarker() },
            { "nd*", _ => new NDEndMarker() },
            { "sup", _ => new SUPMarker() },
            { "sup*", _ => new SUPEndMarker() },
            { "ie", _ => new IEMarker() },
            { "pn", _ => new PNMarker() },
            { "pn*", _ => new PNEndMarker() },
            { "pro", _ => new PROMarker() },
            { "pro*", _ => new PROEndMarker() },

            /* Special Features */
            { "fig", _ => new FIGMarker() },
            { "fig*", _ => new FIGEndMarker() },
            { "\\", _ => new TextBlock("\\") },
        };

        private static Marker SelectMarker(ReadOnlySpan<char> identifier)
        {
            var lookup = MarkerFactories.GetAlternateLookup<ReadOnlySpan<char>>();
            if (lookup.TryGetValue(identifier, out var factory))
            {
                return factory(identifier);
            }
            return new UnknownMarker() { ParsedIdentifier = identifier.ToString() };
        }
    }
}

