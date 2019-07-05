using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public class USFMParser
    {
        private readonly List<string> IgnoredTags;
        public List<UnknownMarker> UnknownMarkers;

        public USFMParser()
        {
            IgnoredTags = new List<string>();
        }

        public USFMParser(List<string> tagsToIgnore)
        {
            IgnoredTags = tagsToIgnore;
        }

        public USFMDocument ParseFromString(string input)
        {
            Regex splitRegex = new Regex("\\\\([a-z0-9*]+)([^\\\\]*)");
            USFMDocument output = new USFMDocument();

            foreach(Match match in splitRegex.Matches(input))
            {
                if(IgnoredTags.Contains(match.Groups[1].Value))
                {
                    continue;
                }

                ConvertToMarkerResult result = ConvertToMarker(match.Groups[1].Value, match.Groups[2].Value);

                if(result.marker is UnknownMarker)
                {
                    UnknownMarkers.Add((UnknownMarker)result.marker);
                }
                else
                {
                    output.Insert(result.marker);

                    if (!string.IsNullOrWhiteSpace(result.remainingText))
                    {
                        output.Insert(new TextBlock(result.remainingText));
                    }
                }
            }

            return output;
        }
        private ConvertToMarkerResult ConvertToMarker(string identifier, string value)
        {
            Marker output = SelectMarker(identifier);
            string tmp = output.PreProcess(value.Trim());
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
                case "h":
                    return new HMarker();
                case "toc1":
                    return new TOC1Marker();
                case "toc2":
                    return new TOC2Marker();
                case "toc3":
                    return new TOC3Marker();
                case "mt":
                case "mt1":
                    return new MTMarker();
                case "mt2":
                    return new MTMarker() { Weight = 2 };
                case "mt3":
                    return new MTMarker() { Weight = 3 };
                case "c":
                    return new CMarker();
                case "p":
                    return new PMarker();
                case "v":
                    return new VMarker();
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
                case "m":
                    return new MMarker();
                case "d":
                    return new DMarker();
                case "ms":
                    return new MSMarker();
                case "cl":
                    return new CLMarker();
                case "qs":
                    return new QSMarker();
                case "qs*":
                    return new QSEndMarker();
                case "f":
                    return new FMarker();
                case "qa":
                    return new QAMarker();
                case "nb":
                    return new NBMarker();
                case "fqa":
                    return new FQAMarker();
                case "pi":
                    return new PIMarker();
                case "sp":
                    return new SPMarker();
                case "ft":
                    return new FTMarker();
                case "fr":
                    return new FRMarker();
                case "fk":
                    return new FKMarker();
                case "fqa*":
                    return new FQAEndMarker();
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
                default:
                    return new UnknownMarker() { ParsedIdentifier = identifier };
            }
        }
    }
}
