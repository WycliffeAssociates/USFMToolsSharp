using System;

namespace USFMToolsSharp.Models.Markers
{
    public class LIKMarker : Marker
    {
        public override string Identifier => "lik";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
