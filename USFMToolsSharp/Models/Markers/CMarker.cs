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
        public int Number;
        public string PublishedChapterMarker
        {
            get
            {
                var childCharacterMarker = GetChildMarkers<CPMarker>();
                if (childCharacterMarker.Count > 0)
                {
                    return childCharacterMarker[0].PublishedChapterMarker;
                }
                else
                {
                    return Number.ToString();
                }
            }
        }
        public string CustomChapterLabel
        {
            get
            {
                var childChapLabelMarker = GetChildMarkers<CLMarker>();
                if (childChapLabelMarker.Count > 0)
                {
                    return childChapLabelMarker[0].Label;
                }
                else
                {
                    return PublishedChapterMarker;
                }

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

        public override HashSet<Type> AllowedContents => AllowedContentsStatic;

        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(MMarker),
            typeof(MSMarker),
            typeof(SMarker),
            typeof(BMarker),
            typeof(DMarker),
            typeof(VMarker),
            typeof(PMarker),
            typeof(PCMarker),
            typeof(PMMarker),
            typeof(CDMarker),
            typeof(CPMarker),
            typeof(DMarker),
            typeof(CLMarker),
            typeof(QMarker),
            typeof(QSMarker),
            typeof(QSEndMarker),
            typeof(QAMarker),
            typeof(QMarker),
            typeof(NBMarker),
            typeof(RMarker),
            typeof(LIMarker),
            typeof(TableBlock),
            typeof(MMarker),
            typeof(MIMarker),
            typeof(PIMarker),
            typeof(CAMarker),
            typeof(CAEndMarker),
            typeof(SPMarker),
            typeof(TextBlock),
            typeof(REMMarker),
            typeof(DMarker),
            typeof(VAMarker),
            typeof(VAEndMarker),
            typeof(FMarker),
            typeof(FEndMarker),
        };
    }
}
