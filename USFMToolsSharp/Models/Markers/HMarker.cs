using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class HMarker : Marker
    {
        public string HeaderText;
        public override string Identifier => "h";

        public override void Populate(string input)
        {
            HeaderText = input;
        }
    }
}
