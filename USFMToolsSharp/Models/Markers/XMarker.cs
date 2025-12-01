using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Cross reference element
    /// </summary>
    public class XMarker : Marker
    {
        public override string Identifier => "x";
        public string CrossRefCaller;

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            CrossRefCaller = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
