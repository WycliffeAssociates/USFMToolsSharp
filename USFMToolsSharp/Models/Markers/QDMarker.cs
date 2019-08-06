using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Hebrew note
    /// </summary>
    public class QDMarker : Marker
    {
        public override string Identifier => "qd";
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
