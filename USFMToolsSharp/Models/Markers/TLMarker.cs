using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Transliterated (or foreign) word(s)
    /// </summary>
    public class TLMarker : Marker
    {
        public override string Identifier => "tl";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock)
        };
    }
}
