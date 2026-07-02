using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Header Marker (Right-Aligned)
    /// </summary>
    public class THRMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "thr";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
