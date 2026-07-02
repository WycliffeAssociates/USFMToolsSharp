using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote verse number
    /// </summary>
    public class FVMarker : Marker
    {
        public string VerseCharacter;
        public override string Identifier => "fv";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            VerseCharacter = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
