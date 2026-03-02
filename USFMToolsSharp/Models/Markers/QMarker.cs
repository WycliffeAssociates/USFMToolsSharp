using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A Poetry Marker
    /// </summary>
    public class QMarker : Marker
    {
        public int Depth = 1;
        public string Text;
        public bool IsPoetryBlock;
        public override string Identifier => "q";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
