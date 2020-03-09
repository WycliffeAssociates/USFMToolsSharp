using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Intented Paragraph marker
    /// </summary>
    public class PIMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "pi";

        public override string PreProcess(string input)
        {
            return input.TrimStart();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(VMarker),
            typeof(BMarker),
            typeof(SPMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(LIMarker),
            typeof(QMarker)
        };
    }
}
