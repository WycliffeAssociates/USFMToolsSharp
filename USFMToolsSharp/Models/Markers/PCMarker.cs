using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Centered paragraph
    /// </summary>
    public class PCMarker : Marker
    {
        public override string Identifier => "pc";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
