using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction outline title
    /// </summary>
    public class IOTMarker : Marker
    {
        public string Title;
        public override string Identifier => "iot";
        public override string PreProcess(string input)
        {
            Title = input.Trim();
            return string.Empty;
        }
    }
}
