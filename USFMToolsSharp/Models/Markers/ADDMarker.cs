using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Translator’s addition
    /// </summary>
    public class ADDMarker : Marker
    {
        public override string Identifier => "add";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
