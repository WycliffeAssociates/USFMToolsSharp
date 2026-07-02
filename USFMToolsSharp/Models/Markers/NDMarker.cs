using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Name of God (name of Deity)
    /// </summary>
    public class NDMarker : Marker
    {
        public override string Identifier => "nd";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
