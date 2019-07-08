using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major section reference marker
    /// </summary>
    public class MRMarker : Marker
    {
        public int Weight = 1;
        public string SectionReference;
        public override string Identifier => "mr";
        public override string PreProcess(string input)
        {
            SectionReference= input;
            return string.Empty;
        }
    }
}
