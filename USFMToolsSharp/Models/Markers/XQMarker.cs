using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A quotation from the scripture text
    /// </summary>
    public class XQMarker : Marker
    {
        public override string Identifier => "xq";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
