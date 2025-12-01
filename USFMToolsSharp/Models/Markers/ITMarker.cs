using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Italic marker
    /// </summary>
    public class ITMarker : Marker
    {
        public override string Identifier => "it";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
