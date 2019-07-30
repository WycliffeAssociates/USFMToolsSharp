using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction section heading
    /// </summary>
    public class ISMarker : Marker
    {
        public int Weight = 1;
        public string Heading;
        public override string Identifier => "is";
        public override string PreProcess(string input)
        {
            Heading = input;
            return string.Empty;
        }
    }
}
