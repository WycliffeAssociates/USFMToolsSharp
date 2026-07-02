using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote translations
    /// </summary>
    public class FQMarker : Marker
    {
        public override string Identifier => "fq";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
