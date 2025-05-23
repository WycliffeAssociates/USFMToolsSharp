using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Embedded text poetic line
    /// </summary>
    public class QMMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "qm";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new()
        {
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
