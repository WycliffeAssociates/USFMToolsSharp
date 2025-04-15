using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote origin reference
    /// </summary>
    public class FRMarker : Marker
    {
        public override string Identifier => "fr";
        public string VerseReference;


        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            VerseReference = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
