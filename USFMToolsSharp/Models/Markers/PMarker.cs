using System;

namespace USFMToolsSharp.Models.Markers
{
    public class PMarker : Marker
    {
        public override string Identifier => "p";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
