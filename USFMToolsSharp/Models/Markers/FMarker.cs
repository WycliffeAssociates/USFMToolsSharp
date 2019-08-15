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
        public override string Identifier => "f";
        public string FootNoteCaller;

        
        public override string PreProcess(string input)
        {
            FootNoteCaller = input;
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(FRMarker),
            typeof(FKMarker),
            typeof(FTMarker),
            typeof(FVMarker),
            typeof(FVEndMarker),
            typeof(FPMarker),
            typeof(FQAMarker),
            typeof(FQMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
            typeof(TextBlock),
        };
    }
}
