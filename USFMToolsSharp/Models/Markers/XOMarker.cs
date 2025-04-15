using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Cross reference origin reference
    /// </summary>
    public class XOMarker : Marker
    {
        public string OriginRef;
        public override string Identifier => "xo";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            OriginRef = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(TextBlock),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
