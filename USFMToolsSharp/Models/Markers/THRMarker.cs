using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Header Marker (Right-Aligned)
    /// </summary>
    public class THRMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "thr";

        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
