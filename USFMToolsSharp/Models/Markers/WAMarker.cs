using System;

namespace USFMToolsSharp.Models.Markers
{
    public class WAMarker : Marker
    {
        public override string Identifier => "wa";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
