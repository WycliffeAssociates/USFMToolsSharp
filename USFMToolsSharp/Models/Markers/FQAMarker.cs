using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote alternative translations
    /// </summary>
    public class FQAMarker : Marker
    {
        public override string Identifier => "fqa";

        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
        };
    }
}
