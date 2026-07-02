using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Indented introduction paragraph
    /// </summary>
    public class IPIMarker : Marker
    {
        public override string Identifier => "ipi";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
