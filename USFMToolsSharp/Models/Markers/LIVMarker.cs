using System;

namespace USFMToolsSharp.Models.Markers
{
    public class LIVMarker : Marker
    {
        public override string Identifier => "liv";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
