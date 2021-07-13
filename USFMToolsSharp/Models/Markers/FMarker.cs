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
            FootNoteCaller = input.Trim();
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(FRMarker),
            typeof(FREndMarker),
            typeof(FKMarker),
            typeof(FTMarker),
            typeof(FVMarker),
            typeof(FVEndMarker),
            typeof(FPMarker),
            typeof(FQAMarker),
            typeof(FQAEndMarker),
            typeof(FQMarker),
            typeof(FQEndMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
            typeof(TextBlock),
            typeof(UnknownMarker),
            typeof(UnknownEndMarker),
            typeof(ITMarker),
            typeof(ITEndMarker),
            typeof(SCMarker),
            typeof(SCEndMarker),
            typeof(SUPMarker),
            typeof(SUPEndMarker),
        };
    }
}
