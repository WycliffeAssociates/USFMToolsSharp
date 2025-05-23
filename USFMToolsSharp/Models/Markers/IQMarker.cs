using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A Poetry Marker within Introduction
    /// </summary>
    public class IQMarker : Marker
    {
        public int Depth = 1;
        public string Text;
        public override string Identifier => "iq";
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
