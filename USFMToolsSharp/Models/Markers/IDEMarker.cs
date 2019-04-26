using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Encoding marker
    /// </summary>
    public class IDEMarker : Marker
    {
        public string Encoding;
        public override string Identifier => "ide";

        public override string PreProcess(string input)
        {
            Encoding = input;
            return string.Empty;
        }
    }
}
