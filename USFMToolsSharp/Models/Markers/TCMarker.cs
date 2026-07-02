using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Cell Marker
    /// </summary>
    public class TCMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "tc";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }

    }
}
