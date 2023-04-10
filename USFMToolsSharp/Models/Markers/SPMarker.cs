using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A speaker Marker (Used mostly in Job and Songs of Solomon)
    /// </summary>
    /// <remarks>See https://ubsicap.github.io/usfm/titles_headings/index.html#sp</remarks>
    public class SPMarker : Marker
    {
        public string Speaker;
        public override string Identifier => "sp";
        public override string PreProcess(string input)
        {
            Speaker = input.Trim();
            return string.Empty;
        }
    }
}
