using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Row Marker
    /// </summary>
    public class TRMarker : Marker
    {
        public override string Identifier => "tr";

        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(TCMarker),
            typeof(THMarker),
            typeof(TCRMarker),
            typeof(THRMarker)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
