using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class IDMarker : Marker
    {
        public string TextIdentifier;
        public override string Identifier => "id";
        public override void Populate(string input)
        {
            TextIdentifier = input;
        }
    }
}
