using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Table Cell Marker (Right-Aligned)
    /// </summary>
    public class TCRMarker : Marker
    {
        public int ColumnPosition = 1;
        public override string Identifier => "tcr";

        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
            typeof(VMarker)
        };
    }
}
