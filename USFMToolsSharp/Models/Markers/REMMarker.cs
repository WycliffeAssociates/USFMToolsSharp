using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class REMMarker : Marker
    {
        public string Comment;
        public override string Identifier => "rem";

        public override void Populate(string input)
        {
            Comment = input;
        }
    }
}
