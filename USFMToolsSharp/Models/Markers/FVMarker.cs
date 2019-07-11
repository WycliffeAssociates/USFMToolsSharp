using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote verse number
    /// </summary>
    public class FVMarker : Marker
    {
        public string VerseCharacter;
        public override string Identifier => "fv";
        public override string PreProcess(string input)
        {
            VerseCharacter = input;
            return string.Empty;
        }
    }
}
