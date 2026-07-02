using System;

namespace USFMToolsSharp.Models.Markers
{
    public class LFMarker : Marker
    {
        public override string Identifier => "lf";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
