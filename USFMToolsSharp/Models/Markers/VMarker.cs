using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    public class VMarker : Marker
    {
        private static readonly SearchValues<char> Numbers = SearchValues.Create("0123456789");
        private static readonly SearchValues<char> NumbersAndBridge = SearchValues.Create("0123456789-");

        // This is a string because of verse bridges. In the future this should have starting and ending verse
        public string VerseNumber;
        public int StartingVerse;
        public int EndingVerse;

        public string VerseCharacter {
            get {
                var firstCharacterMarker = DefaultHierarchyNode?.GetChildMarkers<VPMarker>();
                if (firstCharacterMarker?.Count > 0)
                {
                    return firstCharacterMarker[0].As<VPMarker>().VerseCharacter;
                }
                
                return VerseNumber;
            }
        }
        public override string Identifier => "v";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            var startOfVerseNumber = input.IndexOfAny(Numbers);
            var foundVerseNumber = startOfVerseNumber != -1;
            if (!foundVerseNumber)
            {
                VerseNumber = string.Empty;
                StartingVerse = 0;
                EndingVerse = 0;
                return input.Trim();
            }
            var firstNonNumericAfterNumber = input[startOfVerseNumber..].IndexOfAnyExcept(NumbersAndBridge) + startOfVerseNumber;
            if (firstNonNumericAfterNumber <= 0)
            {
                firstNonNumericAfterNumber = input.Length;
            }
            var verseNumberSpan = input[startOfVerseNumber..firstNonNumericAfterNumber].Trim();
            VerseNumber = verseNumberSpan.ToString();

            var hasBridge = input[startOfVerseNumber..firstNonNumericAfterNumber].Contains('-');
            if (hasBridge)
            {
                var index = verseNumberSpan.IndexOf('-');
                var isBridge = index != -1;
                StartingVerse = int.Parse(isBridge ? verseNumberSpan[..index].Trim(): verseNumberSpan);
                EndingVerse = isBridge && !verseNumberSpan[index..].IsWhiteSpace() ? int.Parse(verseNumberSpan[(index + 1)..].Trim()) : StartingVerse;
            }
            else
            {
                StartingVerse = int.Parse(verseNumberSpan);
                EndingVerse = StartingVerse;
            }
            return input[firstNonNumericAfterNumber..].TrimStart(' ');
        }
    }
}
