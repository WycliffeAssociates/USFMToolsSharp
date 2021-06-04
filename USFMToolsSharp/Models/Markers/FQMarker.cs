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

        public override string PreProcess(string input)
        {
            return input.TrimStart();
        }

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
