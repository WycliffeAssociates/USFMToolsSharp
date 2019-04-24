using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class IDEMarker : Marker
    {
        public string Encoding;
        public override string Identifier => "ide";

        public override void Populate(string input)
        {
            Encoding = input;
        }
    }
}
