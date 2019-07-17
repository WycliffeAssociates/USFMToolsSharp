using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List Entry Marker
    /// </summary>
    public class LIMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "li";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(VMarker),
            typeof(TextBlock)
        };
    }
}
