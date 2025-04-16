using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footer Paragraph Marker
    /// </summary>
    public class FPMarker : Marker
    {
        public override string Identifier => "fp";
        private static HashSet<Type> AllowedContentsStatic { get; } = new ()
        {
            typeof(TextBlock),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
