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
            markers = CleanWhitespace(markers);



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
        private List<Marker> CleanWhitespace(List<Marker> input)
        {
            var output = new List<Marker>();
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
                if(!(input[index - 1].Identifier.EndsWith("*") && !input[index + 1].Identifier.EndsWith("*")))
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
        private List<Marker> TokenizeFromString(string input)
        {
            var output = new List<Marker>();

            foreach (Match match in splitRegex.Matches(input))
            {
                if (IgnoredTags.Contains(match.Groups[1].Value))
                {
                    continue;
                }
                var result = ConvertToMarker(match.Groups[1].Value, match.Groups[2].Value);
                result.marker.Position = match.Index;

                // If this is an unknown marker and we're in Ignore Unknown Marker mode then don't add the marker. We still keep any remaining text though
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
            var output = CommonParserHelpers.SelectMarker(identifier);
            var tmp = output.PreProcess(value);
            return new ConvertToMarkerResult(output, tmp);
        }
    }
}