using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Inline quotation reference(s)
    /// </summary>
    public class RQMarker : Marker
    {
        public override string Identifier => "rq";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
