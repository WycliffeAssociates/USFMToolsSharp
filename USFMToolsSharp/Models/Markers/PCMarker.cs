using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Centered paragraph
    /// </summary>
    public class PCMarker : Marker
    {
        public override string Identifier => "pc";
        public override string PreProcess(string input)
        {
            return input.TrimStart();
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(VMarker),
            typeof(BMarker),
            typeof(SPMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(LIMarker),
            typeof(QMarker),
            typeof(SCMarker),
        };
    }
}
