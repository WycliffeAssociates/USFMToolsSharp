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
        public int Weight = 1; 
        public string Heading;
        public override string Identifier => "ms";
        public override string PreProcess(string input)
        {
            Heading = input.TrimStart();
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(MRMarker)
        };
    }
}
