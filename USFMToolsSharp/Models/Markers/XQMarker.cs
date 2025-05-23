using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A quotation from the scripture text
    /// </summary>
    public class XQMarker : Marker
    {
        public override string Identifier => "xq";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(TextBlock),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
