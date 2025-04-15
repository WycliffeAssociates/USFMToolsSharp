using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major heading marker
    /// </summary>
    public class MSMarker : Marker
    {
        public int Weight = 1; 
        public string Heading;
        public override string Identifier => "ms";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Heading = input.TrimStart().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(MRMarker)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
