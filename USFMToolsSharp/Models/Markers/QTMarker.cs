using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class QTMarker : Marker
    {
        public override string Identifier => "qt";
        public override string PreProcess(string input)
        {
            return input;
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
