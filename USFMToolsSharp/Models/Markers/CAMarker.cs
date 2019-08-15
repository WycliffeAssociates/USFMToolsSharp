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

        public override string Identifier => "ca";
        public override string PreProcess(string input)
        {
            AltChapterNumber = input;
            return string.Empty;
        }
    }
}
