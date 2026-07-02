using System;

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
