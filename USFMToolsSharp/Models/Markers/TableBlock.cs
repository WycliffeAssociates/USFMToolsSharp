using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A "marker" for a table block. This exists so that we can handle table data
    /// </summary>
    public class TableBlock : Marker
    {
        public override string Identifier => string.Empty;

        private static HashSet<Type> AllowedContentsStatic { get; } = new()
        {
            typeof(TRMarker)
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
