using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Hebrew note
    /// </summary>
    public class QDMarker : Marker
    {
        public override string Identifier => "qd";
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
