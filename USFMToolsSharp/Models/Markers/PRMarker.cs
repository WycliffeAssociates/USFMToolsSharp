using System;

namespace USFMToolsSharp.Models.Markers
{
    public class PRMarker : Marker
    {
        public override string Identifier => "pr";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
