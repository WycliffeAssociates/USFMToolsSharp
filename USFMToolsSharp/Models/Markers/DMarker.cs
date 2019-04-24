using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A descriptive title marker
    /// </summary>
    public class DMarker : Marker
    {
        public string Description;
        public override string Identifier => "d";
        public override void Populate(string input)
        {
            Description = input;
        }
    }
}
