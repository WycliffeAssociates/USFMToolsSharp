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

        
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            FootNoteCaller = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        private static HashSet<Type> AllowedContentsStatic => new ()
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
            typeof(ITMarker),
            typeof(ITEndMarker),
            typeof(SCMarker),
            typeof(SCEndMarker),
            typeof(SUPMarker),
            typeof(SUPEndMarker),
            typeof(BKMarker),
            typeof(BKEndMarker),
            typeof(BDMarker),
            typeof(BDEndMarker),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
