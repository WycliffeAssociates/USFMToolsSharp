using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    /// <summary>
    /// Parses a USFM file into a Abstract Syntax Tree
    /// </summary>
    public class USFMParser
    {
        private readonly List<string> IgnoredTags;
        private readonly bool IgnoreUnknownMarkers;
        private static Regex splitRegex = new Regex("\\\\([a-z0-9\\-]*\\**)([^\\\\]*)", RegexOptions.Singleline);


        public USFMParser(List<string> tagsToIgnore = null, bool ignoreUnknownMarkers = false)
        {
            IgnoredTags = tagsToIgnore ?? new List<string>();
            IgnoreUnknownMarkers = ignoreUnknownMarkers;
        }

        /// <summary>
        /// Parses a string into a USFMDocument
        /// </summary>
        /// <param name="input">A USFM string</param>
        /// <returns>A USFMDocument representing the input</returns>
        public USFMDocument ParseFromString(string input)
        {
            USFMDocument output = new USFMDocument();
            var markers = TokenizeFromString(input);

            // Clean out extra whitespace where it isn't needed
            CleanWhitespace(markers);



            for (int markerIndex = 0; markerIndex < markers.Count; markerIndex++)
            {
                Marker marker = markers[markerIndex];
                if (marker is TRMarker && !output.GetTypesPathToLastMarker().Contains(typeof(TableBlock)))
                {
                    output.Insert(new TableBlock());
                }

                if(marker is QMarker && markerIndex != markers.Count - 1 && markers[markerIndex + 1] is VMarker)
                {
                    ((QMarker)marker).IsPoetryBlock = true;
                }
                
                output.Insert(marker);
            }

            return output;
        }

        /// <summary>
        /// Removes all the unessecary whitespace while preserving space between closing markers and opening markers
        /// </summary>
        /// <param name="input"></param>
        private void CleanWhitespace(List<Marker> input)
        {
            var markersToProcess = input.Where(i => i is TextBlock block && string.IsNullOrWhiteSpace(block.Text)).ToList();
            foreach(var marker in markersToProcess)
            {
                var index = input.IndexOf(marker);

                // If this is an empty text block at the beginning remove it
                if(index == 0)
                {
                    input.RemoveAt(index);
                    continue;
                }
                
                // If this is an empty text block at the end then remove it
                if(index == input.Count - 1)
                {
                    input.RemoveAt(index);
                    continue;
                }

                // If this isn't between and end marker and another marker then delete it
                if(!(input[index - 1].Identifier.EndsWith("*") && !input[index + 1].Identifier.EndsWith("*")))
                {
                    input.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Generate a list of Markers from a string
        /// </summary>
        /// <param name="input">USFM String to tokenize</param>
        /// <returns>A List of Markers based upon the string</returns>
        private List<Marker> TokenizeFromString(string input)
        {
            List<Marker> output = new List<Marker>();

            foreach (Match match in splitRegex.Matches(input))
            {
                if (IgnoredTags.Contains(match.Groups[1].Value))
                {
                    continue;
                }
                ConvertToMarkerResult result = ConvertToMarker(match.Groups[1].Value, match.Groups[2].Value);
                result.marker.Position = match.Index;

                // If this is an unkown marker and we're in Ignore Unkown Marker mode then don't add the marker. We still keep any remaining text though
                if (!(result.marker is UnknownMarker) || !IgnoreUnknownMarkers)
                {
                    output.Add(result.marker);
                }

                if (!string.IsNullOrEmpty(result.remainingText))
                {
                    output.Add(new TextBlock(result.remainingText));
                }
            }

            return output;
        }
        private ConvertToMarkerResult ConvertToMarker(string identifier, string value)
        {
            Marker output = SelectMarker(identifier);
            string tmp = output.PreProcess(value);
            return new ConvertToMarkerResult(output, tmp);
        }

        private Marker SelectMarker(string identifier)
        {
            switch (identifier)
            {
                case "id":
                    return new IDMarker();
                case "ide":
                    return new IDEMarker();
                case "sts":
                    return new STSMarker();
                case "h":
                    return new HMarker();
                case "toc1":
                    return new TOC1Marker();
                case "toc2":
                    return new TOC2Marker();
                case "toc3":
                    return new TOC3Marker();
                case "toca1":
                    return new TOCA1Marker();
                case "toca2":
                    return new TOCA2Marker();
                case "toca3":
                    return new TOCA3Marker();

                /* Introduction Markers*/
                case "imt":
                case "imt1":
                    return new IMTMarker();
                case "imt2":
                    return new IMTMarker() { Weight = 2 };
                case "imt3":
                    return new IMTMarker() { Weight = 3 };
                case "is":
                case "is1":
                    return new ISMarker();
                case "is2":
                    return new ISMarker() { Weight = 2 };
                case "is3":
                    return new ISMarker() { Weight = 3 };
                case "ib":
                    return new IBMarker();
                case "iq":
                case "iq1":
                    return new IQMarker();
                case "iq2":
                    return new IQMarker() { Depth = 2 };
                case "iq3":
                    return new IQMarker() { Depth = 3 };
                case "iot":
                    return new IOTMarker();
                case "io":
                case "io1":
                    return new IOMarker();
                case "io2":
                    return new IOMarker() { Depth = 2 };
                case "io3":
                    return new IOMarker() { Depth = 3 };
                case "ior":
                    return new IORMarker();
                case "ior*":
                    return new IOREndMarker();
                case "ili":
                case "ili1":
                    return new ILIMarker();
                case "ili2":
                    return new ILIMarker() { Depth = 2 };
                case "ili3":
                    return new ILIMarker() { Depth = 3 };
                case "ip":
                    return new IPMarker();
                case "ipi":
                    return new IPIMarker();
                case "im":
                    return new IMMarker();
                case "imi":
                    return new IMIMarker();
                case "ipq":
                    return new IPQMarker();
                case "imq":
                    return new IMQMarker();
                case "ipr":
                    return new IPRMarker();
                case "mt":
                case "mt1":
                    return new MTMarker();
                case "mt2":
                    return new MTMarker() { Weight = 2 };
                case "mt3":
                    return new MTMarker() { Weight = 3 };
                case "c":
                    return new CMarker();
                case "cp":
                    return new CPMarker();
                case "ca":
                    return new CAMarker();
                case "ca*":
                    return new CAEndMarker();
                case "p":
                    return new PMarker();
                case "v":
                    return new VMarker();
                case "va":
                    return new VAMarker();
                case "va*":
                    return new VAEndMarker();
                case "vp":
                    return new VPMarker();
                case "vp*":
                    return new VPEndMarker();
                case "q":
                case "q1":
                    return new QMarker();
                case "q2":
                    return new QMarker() { Depth = 2 };
                case "q3":
                    return new QMarker() { Depth = 3 };
                case "q4":
                    return new QMarker() { Depth = 4 };
                case "qr":
                    return new QRMarker();
                case "qc":
                    return new QCMarker();
                case "qd":
                    return new QDMarker();
                case "qac":
                    return new QACMarker();
                case "qac*":
                    return new QACEndMarker();
                case "qm":
                case "qm1":
                    return new QMMarker() { Depth = 1 };
                case "qm2":
                    return new QMMarker() { Depth = 2 };
                case "qm3":
                    return new QMMarker() { Depth = 3 };

                case "m":
                    return new MMarker();
                case "d":
                    return new DMarker();
                case "ms":
                case "ms1":
                    return new MSMarker();
                case "ms2":
                    return new MSMarker() { Weight = 2 };
                case "ms3":
                    return new MSMarker() { Weight = 3 };
                case "mr":
                    return new MRMarker();
                case "cl":
                    return new CLMarker();
                case "qs":
                    return new QSMarker();
                case "qs*":
                    return new QSEndMarker();
                case "f":
                    return new FMarker();
                case "fp":
                    return new FPMarker();
                case "qa":
                    return new QAMarker();
                case "nb":
                    return new NBMarker();
                case "fqa":
                    return new FQAMarker();
                case "fqa*":
                    return new FQAEndMarker();
                case "fq":
                    return new FQMarker();
                case "fq*":
                    return new FQEndMarker();
                case "pi":
                case "pi1":
                    return new PIMarker();
                case "pi2":
                    return new PIMarker() { Depth = 2 };
                case "pi3":
                    return new PIMarker() { Depth = 3 };
                case "sp":
                    return new SPMarker();
                case "ft":
                    return new FTMarker();
                case "fr":
                    return new FRMarker();
                case "fr*":
                    return new FREndMarker();
                case "fk":
                    return new FKMarker();
                case "fv":
                    return new FVMarker();
                case "fv*":
                    return new FVEndMarker();
                case "f*":
                    return new FEndMarker();
                case "bd":
                    return new BDMarker();
                case "bd*":
                    return new BDEndMarker();
                case "it":
                    return new ITMarker();
                case "it*":
                    return new ITEndMarker();
                case "rem":
                    return new REMMarker();
                case "b":
                    return new BMarker();
                case "s":
                case "s1":
                    return new SMarker();
                case "s2":
                    return new SMarker() { Weight = 2 };
                case "s3":
                    return new SMarker() { Weight = 3 };
                case "bk":
                    return new BKMarker();
                case "bk*":
                    return new BKEndMarker();
                case "li":
                case "li1":
                    return new LIMarker();
                case "li2":
                    return new LIMarker() { Depth = 2 };
                case "li3":
                    return new LIMarker() { Depth = 3 };
                case "add":
                    return new ADDMarker();
                case "add*":
                    return new ADDEndMarker();
                case "tl":
                    return new TLMarker();
                case "tl*":
                    return new TLEndMarker();
                case "mi":
                    return new MIMarker();
                case "sc":
                    return new SCMarker();
                case "sc*":
                    return new SCEndMarker();
                case "r":
                    return new RMarker();
                case "rq":
                    return new RQMarker();
                case "rq*":
                    return new RQEndMarker();
                case "w":
                    return new WMarker();
                case "w*":
                    return new WEndMarker();
                case "x":
                    return new XMarker();
                case "x*":
                    return new XEndMarker();
                case "xo":
                    return new XOMarker();
                case "xt":
                    return new XTMarker();
                case "xq":
                    return new XQMarker();
                case "pc":
                    return new PCMarker();
                case "cls":
                    return new CLSMarker();
                case "tr":
                    return new TRMarker();
                case "th1":
                    return new THMarker();
                case "thr1":
                    return new THRMarker();
                case "th2":
                    return new THMarker() { ColumnPosition = 2 };
                case "thr2":
                    return new THRMarker() { ColumnPosition = 2};
                case "th3":
                    return new THMarker() { ColumnPosition = 3 };
                case "thr3":
                    return new THRMarker() { ColumnPosition = 3};
                case "tc1":
                    return new TCMarker();
                case "tcr1":
                    return new TCRMarker();
                case "tc2":
                    return new TCMarker() { ColumnPosition = 2 };
                case "tcr2":
                    return new TCRMarker() { ColumnPosition = 2 };
                case "tc3":
                    return new TCMarker() { ColumnPosition = 3 };
                case "tcr3":
                    return new TCRMarker() { ColumnPosition = 3 };
                case "usfm":
                    return new USFMMarker();
                /* Character Styles */
                case "em":
                    return new EMMarker();
                case "em*":
                    return new EMEndMarker();
                case "bdit":
                    return new BDITMarker();
                case "bdit*":
                    return new BDITEndMarker();
                case "no":
                    return new NOMarker();
                case "no*":
                    return new NOEndMarker();
                case "k":
                    return new KMarker();
                case "k*":
                    return new KEndMarker();
                case "lf":
                    return new LFMarker();
                case "lik":
                    return new LIKMarker();
                case "lik*":
                    return new LIKEndMarker();
                case "litl":
                    return new LITLMarker();
                case "litl*":
                    return new LITLEndMarker();
                case "liv":
                    return new LIVMarker();
                case "liv*":
                    return new LIVEndMarker();
                case "ord":
                    return new ORDMarker();
                case "ord*":
                    return new ORDEndMarker();
                case "pmc":
                    return new PMCMarker();
                case "pmo":
                    return new PMOMarker();
                case "pmr":
                    return new PMRMarker();
                case "png":
                    return new PNGMarker();
                case "png*":
                    return new PNGEndMarker();
                case "pr":
                    return new PRMarker();
                case "qt":
                    return new QTMarker();
                case "qt*":
                    return new QTEndMarker();
                case "rb":
                    return new RBMarker();
                case "rb*":
                    return new RBEndMarker();
                case "sig":
                    return new SIGMarker();
                case "sig*":
                    return new SIGEndMarker();
                case "sls":
                    return new SLSMarker();
                case "sls*":
                    return new SLSEndMarker();
                case "wa":
                    return new WAMarker();
                case "wa*":
                    return new WAEndMarker();
                case "wg":
                    return new WGMarker();
                case "wg*":
                    return new WGEndMarker();
                case "wh":
                    return new WHMarker();
                case "wh*":
                    return new WHEndMarker();
                case "wj":
                    return new WJMarker();
                case "wj*":
                    return new WJEndMarker();
                case "nd":
                    return new NDMarker();
                case "nd*":
                    return new NDEndMarker();
                case "sup":
                    return new SUPMarker();
                case "sup*":
                    return new SUPEndMarker();
                case "ie":
                    return new IEMarker();
                case "pn":
                    return new PNMarker();
                case "pn*":
                    return new PNEndMarker();
                case "pro":
                    return new PROMarker();
                case "pro*":
                    return new PROEndMarker();


                /* Special Features */
                case "fig":
                    return new FIGMarker();
                case "fig*":
                    return new FIGEndMarker();

                default:
                    return new UnknownMarker() { ParsedIdentifier = identifier };
            }
        }
    }
}