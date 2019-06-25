using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major title marker
    /// </summary>
    public class MTMarker : Marker
    {
        public string Title;
        public override string Identifier => "mt";
        public override string PreProcess(string input)
        {
            Title = input;
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(MSMarker),
            typeof(CMarker),
            typeof(CLMarker)
        };
    }
}
