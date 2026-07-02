using System;

namespace USFMToolsSharp.Models.Markers
{
    public class PMOMarker : Marker
    {
        public override string Identifier => "pmo";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        
    }
}
