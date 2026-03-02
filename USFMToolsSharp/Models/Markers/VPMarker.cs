using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Marker for Custom Verse Number
    /// </summary>
    public class VPMarker : Marker
    {
        public string VerseCharacter;
        public override string Identifier => "vp";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            VerseCharacter = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
