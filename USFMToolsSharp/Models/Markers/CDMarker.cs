using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Chapter description marker
    /// </summary>
    public class CDMarker : Marker
    {
        public string Description;
        public override string Identifier => "cd";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Description = input.ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
