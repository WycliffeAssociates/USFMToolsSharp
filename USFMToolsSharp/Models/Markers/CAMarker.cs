using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Alternate chapter number
    /// </summary>
    public class CAMarker : Marker
    {
        public string AltChapterNumber;
        public string AltChapterCharacter
        {
            get
            {
                var firstCharacterMarker = GetChildMarkers<CPMarker>();
                if (firstCharacterMarker.Count > 0)
                {
                    return firstCharacterMarker[0].PublishedChapterMarker;
                }
                else
                {
                    return AltChapterCharacter;
                }
            }
        }
        public override string Identifier => "ca";
        public override string PreProcess(string input)
        {
            AltChapterNumber = input;
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(CPMarker),
        };
    }
}
