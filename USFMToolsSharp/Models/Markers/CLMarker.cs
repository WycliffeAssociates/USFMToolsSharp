using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Chapter label marker
    /// </summary>
    public class CLMarker : Marker
    {
        public string Label;
        public override string Identifier => "cl";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Label = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
