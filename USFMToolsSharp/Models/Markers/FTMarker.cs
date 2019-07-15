using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class FTMarker : Marker
    {
        public override string Identifier => "ft";
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(FQAMarker),
            typeof(FQMarker),
            typeof(WMarker),
            typeof(WEndMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(TextBlock),
        };
    }
}
