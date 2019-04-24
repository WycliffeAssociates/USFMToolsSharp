using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Acrostic heading for poetry
    /// </summary>
    public class QAMarker : Marker
    {
        public string Heading;
        public override string Identifier => "qa";
        public override void Populate(string input)
        {
            Heading = input;
        }
    }
}
