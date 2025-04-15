using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Acrostic heading for poetry
    /// </summary>
    public class QAMarker : Marker
    {
        /// <summary>
        /// Heading for the poetry
        /// </summary>
        public string Heading;
        public override string Identifier => "qa";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Heading = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(QACMarker),
            typeof(QACEndMarker),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
