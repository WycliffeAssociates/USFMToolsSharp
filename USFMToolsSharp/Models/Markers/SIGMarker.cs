using System;

namespace USFMToolsSharp.Models.Markers
{
    public class SIGMarker : Marker
    {
        public override string Identifier => "sig";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
