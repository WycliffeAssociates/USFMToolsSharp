using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class FTMarker : Marker
    {
        public override string Identifier => "ft";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
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
