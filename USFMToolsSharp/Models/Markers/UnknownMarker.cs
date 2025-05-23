using System;
using System.Collections.Generic;
using System.Text;

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
