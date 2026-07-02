using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Hebrew note
    /// </summary>
    public class QDMarker : Marker
    {
        public override string Identifier => "qd";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
    }
}
