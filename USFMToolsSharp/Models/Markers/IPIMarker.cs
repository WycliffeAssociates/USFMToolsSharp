using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Indented introduction paragraph
    /// </summary>
    public class IPIMarker : Marker
    {
        public override string Identifier => "ipi";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
            typeof(BKMarker),
            typeof(BKEndMarker),
            typeof(BDMarker),
            typeof(BDEndMarker),
            typeof(ITMarker),
            typeof(ITEndMarker)

        };

    }
}
