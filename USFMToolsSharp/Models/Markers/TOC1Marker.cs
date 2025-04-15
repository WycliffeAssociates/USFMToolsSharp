using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A tag for the long table of contents
    /// </summary>
    public class TOC1Marker : Marker
    {
        public string LongTableOfContentsText;
        public override string Identifier => "toc1";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            LongTableOfContentsText = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
