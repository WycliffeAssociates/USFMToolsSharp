using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Embedded text poetic line
    /// </summary>
    public class QMMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "qm";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
