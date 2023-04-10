using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major title marker
    /// </summary>
    /// <remarks>See https://ubsicap.github.io/usfm/titles_headings/index.html#mt</remarks>
    public class MTMarker : Marker
    {
        public int Weight = 1;
        public string Title;
        public override string Identifier => "mt";
        public override string PreProcess(string input)
        {
            Title = input.Trim();
            return string.Empty;
        }
    }
}
