using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Bold-italic text
    /// </summary>
    public class BDITMarker : Marker
    {
        public override string Identifier => "bdit";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
