using System;

namespace USFMToolsSharp.Models.Markers
{
    public class PMRMarker : Marker
    {
        public override string Identifier => "pmr";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
