using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Cross reference origin reference
    /// </summary>
    public class XOMarker : Marker
    {
        public string OriginRef;
        public override string Identifier => "xo";
        public override string PreProcess(string input)
        {
            OriginRef = input.Trim();
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
