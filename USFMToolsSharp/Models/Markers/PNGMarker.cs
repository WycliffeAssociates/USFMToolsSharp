using System;

namespace USFMToolsSharp.Models.Markers
{
    public class PNGMarker : Marker
    {
        public override string Identifier => "png";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
