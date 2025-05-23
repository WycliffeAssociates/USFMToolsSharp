using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote keyword Marker
    /// </summary>
    public class FKMarker : Marker
    {
        public override string Identifier => "fk";
        public string FootNoteKeyword;


        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            FootNoteKeyword = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
