using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class FTMarker : Marker
    {
        public override string Identifier => "ft";
        public override string PreProcess(string input)
        {
            return input.TrimStart();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(FQAMarker),
            typeof(FQAEndMarker),
            typeof(FQMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
            typeof(TextBlock),
        };
    }
}
