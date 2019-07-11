﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    public class VMarker : Marker
    {
        // This is a string because of verse bridges. In the future this should have starting and ending verse
        public string VerseNumber;
        private static Regex verseRegex = new Regex("([0-9-]+) (.*)");

        public string VerseCharacter {
            get {
                var firstCharacterMarker = GetChildMarkers<VPMarker>();
                if (firstCharacterMarker.Count > 0)
                {
                    return firstCharacterMarker[0].VerseCharacter;
                }
                else
                {
                    return VerseNumber;
                }
            }
        }
        public override string Identifier => "v";
        public override string PreProcess(string input)
        {
            Match match = verseRegex.Match(input);
            VerseNumber = match.Groups[1].Value;
            return match.Groups[2].Value;
        }

        public override List<Type> AllowedContents => new List<Type>()
                {
                    typeof(VPMarker),
                    typeof(VPEndMarker),
                    typeof(TLMarker),
                    typeof(TLEndMarker),
                    typeof(ADDMarker),
                    typeof(ADDEndMarker),
                    typeof(BMarker),
                    typeof(BKMarker),
                    typeof(BKEndMarker),
                    typeof(BDMarker),
                    typeof(BDEndMarker),
                    typeof(ITMarker),
                    typeof(ITEndMarker),
                    typeof(QMarker),
                    typeof(MMarker),
                    typeof(FMarker),
                    typeof(FEndMarker),
                    typeof(SPMarker),
                    typeof(LIMarker),
                    typeof(TextBlock),
                    typeof(WMarker),
                    typeof(WEndMarker),
                    typeof(XMarker),
                    typeof(XEndMarker)
                    typeof(CLSMarker)
                };
    }
}
