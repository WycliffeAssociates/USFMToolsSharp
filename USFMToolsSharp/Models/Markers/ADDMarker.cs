using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Translator’s addition
    /// </summary>
    public class ADDMarker : Marker
    {
        public override string Identifier => "add";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock),
        };
    }
}
