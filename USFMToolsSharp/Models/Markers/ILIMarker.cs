using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List Entry Marker within Introduction
    /// </summary>
    public class ILIMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "ili";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
