using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Transliterated (or foreign) word(s)
    /// </summary>
    public class TLMarker : Marker
    {
        public override string Identifier => "tl";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
