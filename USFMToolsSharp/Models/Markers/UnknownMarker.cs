using System;

namespace USFMToolsSharp.Models.Markers
{
    public class UnknownMarker: Marker
    {
        public string ParsedIdentifier;
        public string ParsedValue;

        public override string Identifier => string.Empty;
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            ParsedValue = input.ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
