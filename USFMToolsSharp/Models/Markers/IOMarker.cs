using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction outline entry
    /// </summary>
    public class IOMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "io";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
            typeof(IORMarker),
            typeof(IOREndMarker)
        };
    }
}
