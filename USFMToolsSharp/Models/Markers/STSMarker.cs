using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Project text status tracking
    /// </summary>
    public class STSMarker : Marker
    {
        public string StatusText;
        public override string Identifier => "sts";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            StatusText = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
