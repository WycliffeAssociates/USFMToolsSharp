using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Chapter description marker
    /// </summary>
    public class CDMarker : Marker
    {
        public string Description;
        public override string Identifier => "cd";
        public override string PreProcess(string input)
        {
            Description = input;
            return string.Empty;
        }
    }
}
