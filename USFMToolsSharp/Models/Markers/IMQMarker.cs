using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction flush left (margin) quote from text paragraph
    /// </summary>
    public class IMQMarker : Marker
    {
        public override string Identifier => "imq";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
