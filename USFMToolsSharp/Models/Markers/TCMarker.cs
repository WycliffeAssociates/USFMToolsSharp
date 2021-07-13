using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Cell Marker
    /// </summary>
    public class TCMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "tc";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }

        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
            typeof(VMarker)
        };
    }
}
