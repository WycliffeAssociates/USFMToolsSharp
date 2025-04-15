using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for book abbreviation
    /// </summary>
    public class TOC3Marker : Marker
    {
        public string BookAbbreviation;
        public override string Identifier => "toc3";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            BookAbbreviation = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
