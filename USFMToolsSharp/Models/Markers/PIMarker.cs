using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Intented Paragraph marker
    /// </summary>
    public class PIMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "pi";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
