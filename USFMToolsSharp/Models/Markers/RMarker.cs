using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Parallel passage reference(s)
    /// </summary>
    public class RMarker : Marker
    {
        public override string Identifier => "r";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };

    }
}
