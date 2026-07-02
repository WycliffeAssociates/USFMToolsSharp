using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Normal Text
    /// </summary>
    public class NOMarker : Marker
    {
        public override string Identifier => "no";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
