using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A flush left margin marker
    /// </summary>
    public class MMarker : Marker
    {
        public override string Identifier => "m";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(VMarker),
            typeof(TextBlock),
            typeof(BKMarker),
            typeof(BKEndMarker),
            typeof(BDMarker),
            typeof(BDEndMarker),
            typeof(ITMarker),
            typeof(ITEndMarker),
            typeof(SCMarker),
            typeof(SCEndMarker),
            typeof(BDITMarker),
            typeof(BDITEndMarker),
            typeof(NDMarker),
            typeof(NDEndMarker),
            typeof(NOMarker),
            typeof(NOEndMarker),
            typeof(SUPMarker),
            typeof(SUPEndMarker)

        };
    }
}
