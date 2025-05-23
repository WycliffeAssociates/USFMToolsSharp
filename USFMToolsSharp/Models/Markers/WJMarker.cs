using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class WJMarker : Marker
    {
        public override string Identifier => "wj";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(TextBlock)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
