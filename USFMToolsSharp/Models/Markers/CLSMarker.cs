using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Closure of an epistle/letter
    /// </summary>
    public class CLSMarker : Marker
    {
        public override string Identifier => "cls";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock)
        };
    }
}
