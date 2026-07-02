using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction paragraph
    /// </summary>
    public class IPMarker : Marker
    {
        public override string Identifier => "ip";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
