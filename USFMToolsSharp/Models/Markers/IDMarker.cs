using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class IDMarker : Marker
    {
        public string TextIdentifier;
        public override string Identifier => "id";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            TextIdentifier = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
