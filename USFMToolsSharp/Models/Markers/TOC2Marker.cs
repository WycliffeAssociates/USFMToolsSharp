using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for the short table of contents
    /// </summary>
    public class TOC2Marker : Marker
    {
        public string ShortTableOfContentsText;
        public override string Identifier => "toc2";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            ShortTableOfContentsText = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
