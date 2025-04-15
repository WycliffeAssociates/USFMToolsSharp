using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major section reference marker
    /// </summary>
    public class MRMarker : Marker
    {
        public int Weight = 1;
        public string SectionReference;
        public override string Identifier => "mr";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            SectionReference= input.TrimStart().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(FMarker),
            typeof(FEndMarker)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
