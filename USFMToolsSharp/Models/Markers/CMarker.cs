using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Chapter marker
    /// </summary>
    public class CMarker : Marker
    {
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
            Number = int.Parse(input);
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
            typeof(SPMarker)
        };
    }
}
