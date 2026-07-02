using System;

namespace USFMToolsSharp.Models.Markers
{
    public class QTMarker : Marker
    {
        public override string Identifier => "qt";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input;
        }
    }
}
