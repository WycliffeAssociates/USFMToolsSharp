using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Encoding marker
    /// </summary>
    public class IDEMarker : Marker
    {
        public string Encoding;
        public override string Identifier => "ide";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Encoding = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
