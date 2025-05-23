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
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(TextBlock),
            typeof(IORMarker),
            typeof(IOREndMarker)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
