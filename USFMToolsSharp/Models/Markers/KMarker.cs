using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Normal Text
    /// </summary>
    public class KMarker : Marker
    {
        public override string Identifier => "k";
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
