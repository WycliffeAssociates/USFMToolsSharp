using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A quotation from the scripture text
    /// </summary>
    public class XQMarker : Marker
    {
        public override string Identifier => "xq";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
