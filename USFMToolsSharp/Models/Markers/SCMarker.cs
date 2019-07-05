using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Small-Cap Letter 
    /// </summary>
    public class SCMarker : Marker
    {
        public override string Identifier => "sc";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
