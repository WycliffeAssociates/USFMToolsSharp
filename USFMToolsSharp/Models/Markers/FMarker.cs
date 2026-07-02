using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote marker
    /// </summary>
    public class FMarker : Marker
    {
        public override string Identifier => "f";
        public string FootNoteCaller;

        
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            FootNoteCaller = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
