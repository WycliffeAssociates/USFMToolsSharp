using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major heading marker
    /// </summary>
    public class MSMarker : Marker
    {
        public string Heading;
        public override string Identifier => "ms";
        public override void Populate(string input)
        {
            Heading = input;
        }
    }
}
