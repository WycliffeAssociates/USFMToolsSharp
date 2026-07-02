using System;

namespace USFMToolsSharp.Models.Markers
{
    public class ORDMarker : Marker
    {
        public override string Identifier => "ord";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
