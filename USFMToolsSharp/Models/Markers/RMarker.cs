using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Parallel passage reference(s)
    /// </summary>
    public class RMarker : Marker
    {
        public override string Identifier => "r";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
