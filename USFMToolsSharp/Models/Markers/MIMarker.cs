using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Indented flush left paragraph
    /// </summary>
    public class MIMarker : Marker
    {
        public override string Identifier => "mi";
        private static HashSet<Type> AllowedContentsStatic => new ()
        {
            typeof(TextBlock),
            typeof(VMarker),
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
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
