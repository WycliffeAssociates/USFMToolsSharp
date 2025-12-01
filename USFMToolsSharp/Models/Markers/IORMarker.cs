using System;


namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction outline reference range
    /// </summary>
    public class IORMarker : Marker
    {
        public override string Identifier => "ior";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
