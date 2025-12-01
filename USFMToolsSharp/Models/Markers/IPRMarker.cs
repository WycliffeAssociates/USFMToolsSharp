using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction right-aligned paragraph
    /// </summary>
    public class IPRMarker : Marker
    {
        public override string Identifier => "ipr";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
