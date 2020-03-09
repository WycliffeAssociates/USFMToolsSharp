using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Superscript text. Typically for use in critical edition footnotes
    /// </summary>
    public class SUPMarker : Marker
    {
        public override string Identifier => "sup";

        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
