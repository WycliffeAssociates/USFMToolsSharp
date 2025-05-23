using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Running header marker
    /// </summary>
    public class HMarker : Marker
    {
        public string HeaderText;
        public override string Identifier => "h";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            HeaderText = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
        
    }
}
