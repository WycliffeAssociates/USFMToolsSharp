using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    public partial class VMarker : Marker
    {
        // This is a string because of verse bridges. In the future this should have starting and ending verse
        public string VerseNumber;
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
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            var match = CreateRegex().Match(input.ToString());
            var verseNumberSpan = match.Groups[1].ValueSpan;
            VerseNumber = verseNumberSpan.ToString();
            if (!string.IsNullOrWhiteSpace(VerseNumber))
            {
                var index = verseNumberSpan.IndexOf('-');
                var isBridge = index != -1;
                StartingVerse = int.Parse(isBridge ? verseNumberSpan[..index]: verseNumberSpan);
                EndingVerse = isBridge && !verseNumberSpan[index..].IsWhiteSpace() ? int.Parse(verseNumberSpan[(index + 1)..]) : StartingVerse;
            }
            return match.Groups[2].ValueSpan;
        }

        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
        private static HashSet<Type> AllowedContentsStatic { get; } = new()
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
                    typeof(FRMarker),
                    typeof(FREndMarker),
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
                    typeof(VAEndMarker),
                    typeof(KMarker),
                    typeof(KEndMarker),
                    typeof(LFMarker),
                    typeof(LIKMarker),
                    typeof(LIKEndMarker),
                    typeof(LITLMarker),
                    typeof(LITLEndMarker),
                    typeof(LIVMarker),
                    typeof(LIMarker),
                    typeof(LIVEndMarker),
                    typeof(ORDMarker),
                    typeof(ORDEndMarker),
                    typeof(PMCMarker),
                    typeof(PMOMarker),
                    typeof(PMRMarker),
                    typeof(PNMarker),
                    typeof(PNEndMarker),
                    typeof(PNGMarker),
                    typeof(PNGEndMarker),
                    typeof(PRMarker),
                    typeof(QTMarker),
                    typeof(QTEndMarker),
                    typeof(RBMarker),
                    typeof(RBEndMarker),
                    typeof(SIGMarker),
                    typeof(SIGEndMarker),
                    typeof(SLSMarker),
                    typeof(SLSEndMarker),
                    typeof(WAMarker),
                    typeof(WAEndMarker),
                    typeof(WGMarker),
                    typeof(WGEndMarker),
                    typeof(WHMarker),
                    typeof(WHEndMarker),
                    typeof(WJMarker),
                    typeof(WJEndMarker),
                    typeof(FIGMarker),
                    typeof(FIGEndMarker),
                    typeof(PNMarker),
                    typeof(PNEndMarker),
                    typeof(PROMarker),
                    typeof(PROEndMarker),
                    typeof(REMMarker),
                    typeof(PMarker),
                    typeof(LIMarker),
                    typeof(PCMarker),
                    typeof(TableBlock)
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

        [GeneratedRegex("^ *([0-9]*-?[0-9]*) ?(.*)", RegexOptions.Compiled | RegexOptions.Singleline)]
        private static partial Regex CreateRegex();
    }
}
