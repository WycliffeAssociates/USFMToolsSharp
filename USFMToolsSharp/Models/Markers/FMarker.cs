using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote marker
    /// </summary>
    public class FMarker : Marker
    {
        // TODO: Flesh this out a bit better once I understand them a bit better
        public override string Identifier => "f";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(FTMarker),
            typeof(FQAMarker),
            typeof(FQAEndMarker),
        };
    }
}
