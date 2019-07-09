using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footer Paragraph Marker
    /// </summary>
    public class FPMarker : Marker
    {
        public override string Identifier => "fp";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
        };
    }
}
