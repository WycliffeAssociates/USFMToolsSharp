using System;

namespace USFMToolsSharp.Models.Markers
{
    public class FTMarker : Marker
    {
        public override string Identifier => "ft";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
