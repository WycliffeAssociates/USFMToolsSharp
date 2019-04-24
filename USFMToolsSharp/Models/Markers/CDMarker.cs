using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class CDMarker : Marker
    {
        public string Description;
        public override string Identifier => "cd";
        public override void Populate(string input)
        {
            Description = input;
        }
    }
}
