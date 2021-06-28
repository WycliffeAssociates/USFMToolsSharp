using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Section marker
    /// </summary>
    public class SMarker : Marker
    {
        public int Weight = 1;
        public string Text;
        public override string Identifier => "s";
        public override string PreProcess(string input)
        {
            Text = input.TrimStart();
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(RMarker),
            typeof(FMarker),
            typeof(FEndMarker),
        };
    }
}
