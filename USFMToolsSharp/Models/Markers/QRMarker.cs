using System;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Right-aligned poetic line
    /// </summary>
    public class QRMarker : Marker
    {
        public override string Identifier => "qr";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
        
            
    }
}
