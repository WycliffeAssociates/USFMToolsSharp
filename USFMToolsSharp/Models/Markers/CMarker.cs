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
        public override string PreProcess(string input)
        {
            Number = int.Parse(input);
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(MMarker),
            typeof(SMarker),
            typeof(DMarker),
            typeof(VMarker),
            typeof(PMarker),
            typeof(PCMarker),
            typeof(CDMarker),
            typeof(CPMarker),
            typeof(DMarker),
            typeof(CLMarker),
            typeof(QSMarker),
            typeof(QSEndMarker),
            typeof(QAMarker),
            typeof(QMarker),
            typeof(NBMarker),
            typeof(RMarker)
        };
    }
}
