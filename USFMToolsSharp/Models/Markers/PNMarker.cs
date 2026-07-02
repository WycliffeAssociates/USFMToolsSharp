using System;


namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Name of God (name of Deity)
    /// </summary>
    
    public class PNMarker : Marker
    {
        public override string Identifier => "pn";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
