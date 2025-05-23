using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class PMarker : Marker
    {
        public override string Identifier => "p";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }

        public override HashSet<Type> AllowedContents => AllowedContentsStatic;

        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(VMarker),
            typeof(BMarker),
            typeof(SPMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(LIMarker),
            typeof(QMarker),
            typeof(XMarker),
            typeof(SCMarker),
        };
    }
}
