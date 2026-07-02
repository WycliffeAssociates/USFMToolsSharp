using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Centered poetic line
    /// </summary>
    public class QCMarker : Marker
    {
        public override string Identifier => "qc";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }

    }
}
