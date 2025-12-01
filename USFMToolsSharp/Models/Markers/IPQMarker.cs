using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction quote from text paragraph
    /// </summary>
    public class IPQMarker : Marker
    {
        public override string Identifier => "ipq";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
