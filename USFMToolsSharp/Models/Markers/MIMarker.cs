using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Indented flush left paragraph
    /// </summary>
    public class MIMarker : Marker
    {
        public override string Identifier => "mi";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
