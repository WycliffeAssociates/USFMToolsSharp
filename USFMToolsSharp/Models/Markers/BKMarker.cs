using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Quoted Book Title Marker
    /// </summary>
    public class BKMarker : Marker
    {
        public string BookTitle;
        public override string Identifier => "bk";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            BookTitle = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
