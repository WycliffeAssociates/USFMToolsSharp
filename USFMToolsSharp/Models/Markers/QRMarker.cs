using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Right-aligned poetic line
    /// </summary>
    public class QRMarker : Marker
    {
        public override string Identifier => "qr";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),

        };
        
            
    }
}
