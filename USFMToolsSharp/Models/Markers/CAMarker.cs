using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Alternate chapter number
    /// </summary>
    public class CAMarker : Marker
    {
        public string AltChapterNumber;

        public override string Identifier => "ca";
        
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            AltChapterNumber = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
