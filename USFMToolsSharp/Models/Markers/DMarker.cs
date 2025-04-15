using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A descriptive title marker
    /// </summary>
    public class DMarker : Marker
    {
        public string Description;
        public override string Identifier => "d";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Description = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        private static HashSet<Type> AllowedContentsStatic => new () {
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(ITMarker),
            typeof(ITEndMarker),
            typeof(TextBlock),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
        
    }
}
