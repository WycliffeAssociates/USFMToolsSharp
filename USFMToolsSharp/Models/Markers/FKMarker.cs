using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote keyword Marker
    /// </summary>
    class FKMarker : Marker
    {
        public override string Identifier => "fk";
        public string FootNoteKeyword;


        public override string PreProcess(string input)
        {
            FootNoteKeyword = input.ToUpper();
            return string.Empty;
        }
    }
}
