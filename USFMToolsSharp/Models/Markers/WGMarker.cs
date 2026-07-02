using System;

namespace USFMToolsSharp.Models.Markers
{
    public class WGMarker : Marker
    {
        public override string Identifier => "wg";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
