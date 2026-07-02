using System;

namespace USFMToolsSharp.Models.Markers
{
    public class WHMarker : Marker
    {
        public override string Identifier => "wh";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
    
}
