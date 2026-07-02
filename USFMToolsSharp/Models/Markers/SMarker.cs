using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Section marker
    /// </summary>
    public class SMarker : Marker
    {
        public int Weight = 1;
        public string Text;
        public override string Identifier => "s";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Text = input.TrimStart().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
