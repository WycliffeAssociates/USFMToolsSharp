using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class QTMarker : Marker
    {
        public override string Identifier => "qt";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input;
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(TextBlock)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
