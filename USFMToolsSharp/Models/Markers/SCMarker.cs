using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Small-Cap Letter 
    /// </summary>
    public class SCMarker : Marker
    {
        public override string Identifier => "sc";
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
