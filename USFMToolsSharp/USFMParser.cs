using System;
using System.Text.RegularExpressions;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public class USFMParser
    {
        public Book ParseFromString(string input)
        {
            Regex splitRegex = new Regex("\\\\(\\S+)(.*)");
            Book output = new Book();

            foreach(Match match in splitRegex.Matches(input))
            {
                //Skip s5 for the time being
                if(match.Groups[1].Value == "s5")
                {
                    continue;
                }
                Marker tmp = ConvertToMarker(match.Groups[1].Value, match.Groups[2].Value);
                if (!output.TryInsert(tmp))
                {
                    // Since this is the root then add them anyway
                    output.Contents.Add(tmp);
                }
            }

            return output;
        }
        private Marker ConvertToMarker(string identifier, string value)
        {
            Marker output = SelectMarker(identifier);
            output.Populate(value.TrimStart());
            return output;
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
                    return new MTMarker();
                case "c":
                    return new CMarker();
                case "p":
                    return new PMarker();
                case "v":
                    return new VMarker();
                default:
                    return new UnknownMarker();
            }
        }
    }
}
