using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Section marker
    /// </summary>
    public class SMarker : Marker
    {
        public string Text;
        public override string Identifier => "s";
        public override string PreProcess(string input)
        {
            Text = input;
            return string.Empty;
        }
    }
}
