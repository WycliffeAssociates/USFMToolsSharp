using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote translations
    /// </summary>
    public class FQMarker : Marker
    {
        public override string Identifier => "fq";

        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
            typeof(WMarker),
            typeof(WEndMarker),
        };
    }
}
