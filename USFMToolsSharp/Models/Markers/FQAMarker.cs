using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote alternative translations
    /// </summary>
    public class FQAMarker : Marker
    {
        public override string Identifier => "fqa";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
