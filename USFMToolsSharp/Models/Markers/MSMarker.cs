using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major heading marker
    /// </summary>
    public class MSMarker : Marker
    {
        public int Weight = 1; 
        public string Heading;
        public override string Identifier => "ms";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Heading = input.TrimStart().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
