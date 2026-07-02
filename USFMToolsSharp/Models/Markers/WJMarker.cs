using System;

namespace USFMToolsSharp.Models.Markers
{
    public class WJMarker : Marker
    {
        public override string Identifier => "wj";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
