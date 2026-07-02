using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction flush left (margin) paragraph
    /// </summary>
    public class IMMarker : Marker
    {
        public override string Identifier => "im";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
