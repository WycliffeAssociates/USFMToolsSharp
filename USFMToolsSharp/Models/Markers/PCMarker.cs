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
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new()
        {
            typeof(VMarker),
            typeof(BMarker),
            typeof(SPMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(LIMarker),
            typeof(QMarker),
            typeof(SCMarker),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
