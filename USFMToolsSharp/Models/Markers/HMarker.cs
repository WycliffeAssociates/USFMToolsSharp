using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Running header marker
    /// </summary>
    public class HMarker : Marker
    {
        public string HeaderText;
        public override string Identifier => "h";

        public override string PreProcess(string input)
        {
            HeaderText = input;
            return string.Empty;
        }
        
    }
}
