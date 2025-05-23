using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote translations
    /// </summary>
    public class FQMarker : Marker
    {
        public override string Identifier => "fq";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }

        private static HashSet<Type> AllowedContentsStatic { get; } = new()
        {
            typeof(TextBlock),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
