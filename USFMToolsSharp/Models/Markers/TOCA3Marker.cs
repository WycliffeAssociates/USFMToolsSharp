using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for alternative language book abbreviation
    /// </summary>
    public class TOCA3Marker : Marker
    {
        public string AltBookAbbreviation;
        public override string Identifier => "toca3";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            AltBookAbbreviation = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
