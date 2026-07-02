using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Header Marker
    /// </summary>
    public class THMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "th";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
