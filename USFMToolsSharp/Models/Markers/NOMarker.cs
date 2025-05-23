using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Normal Text
    /// </summary>
    public class NOMarker : Marker
    {
        public override string Identifier => "no";
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
