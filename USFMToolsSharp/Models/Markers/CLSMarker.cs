using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Closure of an epistle/letter
    /// </summary>
    public class CLSMarker : Marker
    {
        public override string Identifier => "cls";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
