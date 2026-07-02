using System;

namespace USFMToolsSharp.Models.Markers
{
    public class PMCMarker : Marker
    {
        public override string Identifier => "pmc";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
