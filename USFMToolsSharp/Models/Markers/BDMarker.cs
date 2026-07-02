using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Bold Marker
    /// </summary>
    public class BDMarker : Marker
    {
        /// <summary>
        /// Text that is bolded
        /// </summary>
        public string Text;
        public override string Identifier => "bd";
        
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
