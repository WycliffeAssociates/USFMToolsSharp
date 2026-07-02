using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Name of God (name of Deity)
    /// </summary>
    public class PROMarker : Marker
    {
        public override string Identifier => "pro";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
