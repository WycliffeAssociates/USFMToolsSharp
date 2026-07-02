using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Target reference(s)
    /// </summary>
    public class XTMarker : Marker
    {
        public override string Identifier => "xt";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
