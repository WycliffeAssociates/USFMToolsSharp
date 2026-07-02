using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Emphasis text
    /// </summary>
    public class EMMarker : Marker
    {
        public override string Identifier => "em";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
