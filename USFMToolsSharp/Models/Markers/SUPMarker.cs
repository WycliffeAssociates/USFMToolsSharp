using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Superscript text. Typically for use in critical edition footnotes
    /// </summary>
    public class SUPMarker : Marker
    {
        public override string Identifier => "sup";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
