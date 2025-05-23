using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Indented introduction flush left (margin) paragraph
    /// </summary>
    public class IMIMarker : Marker
    {
        public override string Identifier => "imi";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new()
        {
            typeof(TextBlock),
            typeof(BKMarker),
            typeof(BKEndMarker),
            typeof(BDMarker),
            typeof(BDEndMarker),
            typeof(ITMarker),
            typeof(ITEndMarker)

        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;

    }
}
