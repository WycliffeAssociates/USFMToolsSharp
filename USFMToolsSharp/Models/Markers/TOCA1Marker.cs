using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A tag for the alternative language long table of contents
    /// </summary>
    public class TOCA1Marker : Marker
    {
        public string AltLongTableOfContentsText;
        public override string Identifier => "toca1";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            AltLongTableOfContentsText = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
