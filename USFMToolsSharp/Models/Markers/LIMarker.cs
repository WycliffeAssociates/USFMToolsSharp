using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List Entry Marker
    /// </summary>
    public class LIMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "li";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
