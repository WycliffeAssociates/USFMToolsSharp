using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A speaker Marker (Used mostly in Job and Songs of Solomon)
    /// </summary>
    public class SPMarker : Marker
    {
        public string Speaker;
        public override string Identifier => "sp";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Speaker = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
