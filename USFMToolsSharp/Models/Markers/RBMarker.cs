using System;

namespace USFMToolsSharp.Models.Markers
{
    public class RBMarker : Marker
    {
        public override string Identifier => "rb";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
