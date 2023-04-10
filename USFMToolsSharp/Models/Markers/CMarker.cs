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
        private static Regex regex = new Regex(" *(\\d*) *(.*)", RegexOptions.Singleline);
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
        public override string PreProcess(string input)
        {
            var match = regex.Match(input);
            if (match.Success)
            {
                if (string.IsNullOrWhiteSpace(match.Groups[1].Value))
                {
                    Number = 0;
                }
                else
                {
                    Number = int.Parse(match.Groups[1].Value);
                }
                if (string.IsNullOrWhiteSpace(match.Groups[2].Value))
                {
                    return string.Empty;
                }
                return match.Groups[2].Value.TrimEnd();
            }
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(MMarker),
            typeof(MSMarker),
            typeof(SMarker),
            typeof(BMarker),
            typeof(DMarker),
            typeof(VMarker),
            typeof(PMarker),
            typeof(PCMarker),
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
            typeof(PMMarker),
            typeof(LHMarker),
            typeof(XMarker),
            typeof(XEndMarker),
            typeof(XTMarker),
        };
    }
}
