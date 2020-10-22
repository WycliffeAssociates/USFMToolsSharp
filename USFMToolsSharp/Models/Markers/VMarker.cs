using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    public class VMarker : Marker
    {
        // This is a string because of verse bridges. In the future this should have starting and ending verse
        public string VerseNumber;
        private static Regex verseRegex = new Regex("^ *([0-9]*-?[0-9]*) ?(.*)", RegexOptions.Singleline);
        public int StartingVerse;
        public int EndingVerse;

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
            if (!string.IsNullOrWhiteSpace(VerseNumber))
            {
                var verseBridgeChars = VerseNumber.Split('-');
                StartingVerse = int.Parse(verseBridgeChars[0]);
                EndingVerse = verseBridgeChars.Length > 1 && !string.IsNullOrWhiteSpace(verseBridgeChars[1]) ? int.Parse(verseBridgeChars[1]) : StartingVerse;
            }
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
                    typeof(EMMarker),
                    typeof(EMEndMarker),
                    typeof(BDITMarker),
                    typeof(BDITEndMarker),
                    typeof(SUPMarker),
                    typeof(SUPEndMarker),
                    typeof(NOMarker),
                    typeof(NOEndMarker),
                    typeof(SCMarker),
                    typeof(SCEndMarker),
                    typeof(NDMarker),
                    typeof(NDEndMarker),
                    typeof(QMarker),
                    typeof(MMarker),
                    typeof(FMarker),
                    typeof(FEndMarker),
                    typeof(SPMarker),
                    typeof(TextBlock),
                    typeof(WMarker),
                    typeof(WEndMarker),
                    typeof(XMarker),
                    typeof(XEndMarker),
                    typeof(CLSMarker),
                    typeof(RQMarker),
                    typeof(RQEndMarker),
                    typeof(PIMarker),
                    typeof(MIMarker),
                    typeof(QSMarker),
                    typeof(QSEndMarker),
                    typeof(QRMarker),
                    typeof(QCMarker),
                    typeof(QDMarker),
                    typeof(QACMarker),
                    typeof(QACEndMarker),
                    typeof(SMarker),
                    typeof(VAMarker),
                    typeof(VAEndMarker)
                };
        public override bool TryInsert(Marker input)
        {
            if (input is VMarker)
            {
                return false;
            }

            if (input is QMarker poetryMarker && poetryMarker.IsPoetryBlock)
            {
                return false;
            }

            return base.TryInsert(input);
        }
    }
}
