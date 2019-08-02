using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Alternate verse number
    /// </summary>
    public class VAMarker : Marker
    {
        public string AltVerseNumber;
        public string AltVerseCharacter
        {
            get
            {
                var firstCharacterMarker = GetChildMarkers<VPMarker>();
                if (firstCharacterMarker.Count > 0)
                {
                    return firstCharacterMarker[0].VerseCharacter;
                }
                else
                {
                    return AltVerseNumber;
                }
            }
        }
        public override string Identifier => "va";
        public override string PreProcess(string input)
        {
            AltVerseNumber = input;
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(VPMarker),
            typeof(VPEndMarker)
        };
    }
}
