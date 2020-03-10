using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class IDMarker : Marker
    {
        public string TextIdentifier;
        public override string Identifier => "id";
        public override string PreProcess(string input)
        {
            TextIdentifier = input.Trim();
            return string.Empty;
        }
    }
}
