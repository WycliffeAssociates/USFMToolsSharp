using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Indented introduction flush left (margin) paragraph
    /// </summary>
    public class IMIMarker : Marker
    {
        public override string Identifier => "imi";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
