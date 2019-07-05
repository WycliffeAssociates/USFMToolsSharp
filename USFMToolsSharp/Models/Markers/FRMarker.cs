using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote origin reference
    /// </summary>
    class FRMarker : Marker
    {
        public override string Identifier => "fr";
        public string VerseReference;


        public override string PreProcess(string input)
        {
            VerseReference = input;
            return string.Empty;
        }
    }
}
