using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Alternate verse number
    /// </summary>
    public class VAMarker : Marker
    {
        public string AltVerseNumber;
        
        public override string Identifier => "va";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            AltVerseNumber = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
