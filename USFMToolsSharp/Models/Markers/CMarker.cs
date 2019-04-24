using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Chapter marker
    /// </summary>
    public class CMarker : Marker
    {
        public int Number;
        public override string Identifier => "c";
        public override void Populate(string input)
        {
            Number = int.Parse(input);
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(VMarker),
            typeof(PMarker),
            typeof(CDMarker),
            typeof(CPMarker),
            typeof(DMarker),
            typeof(CLMarker),
            typeof(QAMarker),
            typeof(QMarker),
            typeof(NBMarker),
        };
    }
}
