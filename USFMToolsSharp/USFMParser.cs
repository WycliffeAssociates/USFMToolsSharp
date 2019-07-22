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

                if(result.marker is TRMarker && !output.GetTypesPathToLastMarker().Contains(typeof(TableBlock)))
                {
                    output.Insert(new TableBlock());
                }

                
                output.Insert(result.marker);

                if (!string.IsNullOrWhiteSpace(result.remainingText))
                {
                    output.Insert(new TextBlock(result.remainingText));
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
                    return new PIMarker();
                case "sp":
                    return new SPMarker();
                case "ft":
                    return new FTMarker();
                case "fr":
                    return new FRMarker();
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
                default:
                    return new UnknownMarker() { ParsedIdentifier = identifier };
            }
        }
    }
}
