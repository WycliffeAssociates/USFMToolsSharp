using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction major title 
    /// </summary>
    public class IMTMarker : Marker
    {
        public int Weight = 1;
        public string IntroTitle;
        public override string Identifier => "imt";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            IntroTitle = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
