using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A descriptive title marker
    /// </summary>
    public class DMarker : Marker
    {
        public string Description;
        public override string Identifier => "d";
        public override string PreProcess(string input)
        {
            Description = input.Trim();
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(FMarker),
            typeof(FEndMarker)
        };
    }
}
