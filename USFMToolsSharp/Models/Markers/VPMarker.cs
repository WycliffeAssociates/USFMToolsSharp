using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Marker for Custom Verse Number
    /// </summary>
    public class VPMarker : Marker
    {
        public string VerseCharacter;
        public override string Identifier => "vp";
        public override string PreProcess(string input)
        {
            VerseCharacter = input.Trim();
            return string.Empty;
        }
    }
}
