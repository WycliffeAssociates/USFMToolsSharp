using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Chapter marker
    /// </summary>
    public class CMarker : Marker
    {
        private static readonly System.Buffers.SearchValues<char> Numbers = System.Buffers.SearchValues.Create("0123456789");
        public int Number { get; set; } = 0;
        public string PublishedChapterMarker
        {
            get
            {
                var childCharacterMarker = DefaultHierarchyNode?.GetChildMarkers<CPMarker>();
                if (childCharacterMarker?.Count > 0)
                {
                    return childCharacterMarker[0].As<CPMarker>().PublishedChapterMarker;
                }
                
                return Number.ToString();
            }
        }
        public string CustomChapterLabel
        {
            get
            {
                var childChapLabelMarker = DefaultHierarchyNode?.GetChildMarkers<CLMarker>();
                if (childChapLabelMarker?.Count > 0)
                {
                    return childChapLabelMarker[0].As<CLMarker>().Label;
                }
                
                return PublishedChapterMarker;

            }
        }
        public override string Identifier => "c";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            var startOfChapterNumber = input.IndexOfAny(Numbers);
            var foundChapterNumber = startOfChapterNumber != -1;
            if (!foundChapterNumber)
            {
                Number = 0;
                return input.Trim();
            }
            var firstBlankAfterNumber = input[startOfChapterNumber..].IndexOf(' ') + startOfChapterNumber;
            if (firstBlankAfterNumber <= 0)
            {
                firstBlankAfterNumber = input.Length;
            }

            Number = int.Parse(input[startOfChapterNumber..firstBlankAfterNumber]);

            return input[firstBlankAfterNumber..].Trim();
        }
    }
}
