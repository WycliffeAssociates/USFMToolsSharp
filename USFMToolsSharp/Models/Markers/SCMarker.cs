using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Small-Cap Letter 
    /// </summary>
    public class SCMarker : Marker
    {
        public override string Identifier => "sc";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
