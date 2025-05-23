using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Header Marker
    /// </summary>
    public class THMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "th";
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
