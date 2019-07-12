using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Centered paragraph
    /// </summary>
    public class PCMarker : Marker
    {
        public override string Identifier => "pc";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
            typeof(SCMarker),
            typeof(SCEndMarker)
        };
    }
}
