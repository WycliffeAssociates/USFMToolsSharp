using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Marker to indicate the acrostic letter within a poetic line
    /// </summary>
    public class QACMarker : Marker
    {
        public string AcrosticLetter;
        public override string Identifier => "qac";
        public override string PreProcess(string input)
        {
            AcrosticLetter = input.Trim();
            return string.Empty;
        }
    }
}
