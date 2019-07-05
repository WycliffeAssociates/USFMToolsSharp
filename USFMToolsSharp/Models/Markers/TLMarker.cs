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
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock)
        };
    }
}
