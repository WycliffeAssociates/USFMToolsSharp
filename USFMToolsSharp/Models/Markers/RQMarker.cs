using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Inline quotation reference(s)
    /// </summary>
    public class RQMarker : Marker
    {
        public override string Identifier => "rq";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
