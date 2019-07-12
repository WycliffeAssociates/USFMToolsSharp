using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Header Marker
    /// </summary>
    public class THMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "th";

        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
