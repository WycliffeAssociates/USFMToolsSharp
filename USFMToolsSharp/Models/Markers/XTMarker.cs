using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Target reference(s)
    /// </summary>
    public class XTMarker : Marker
    {
        public override string Identifier => "xt";
        public override string PreProcess(string input)
        {
            return input.TrimStart();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
