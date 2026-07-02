using System;

namespace USFMToolsSharp.Models.Markers
{
    public class LITLMarker : Marker
    {
        public override string Identifier => "litl";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
