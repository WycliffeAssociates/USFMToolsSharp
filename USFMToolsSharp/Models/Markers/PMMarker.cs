using System;
using System.Collections.Generic;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Embedded text paragraph
    /// </summary>
    /// <remarks>See documentation at https://ubsicap.github.io/usfm/paragraphs/index.html#pm</remarks>
    public class PMMarker: Marker
    {
        public override string Identifier => "pm";

        public override List<Type> AllowedContents => new()
        {
            typeof(VMarker),
            typeof(BMarker),
            typeof(SPMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(LIMarker),
            typeof(QMarker),
            typeof(XMarker),
        };
    }
}