using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Project text status tracking
    /// </summary>
    public class STSMarker : Marker
    {
        public string StatusText;
        public override string Identifier => "sts";
        public override string PreProcess(string input)
        {
            StatusText = input;
            return string.Empty;
        }
    }
}
