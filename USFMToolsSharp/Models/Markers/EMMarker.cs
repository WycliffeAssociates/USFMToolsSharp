using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Emphasis text
    /// </summary>
    public class EMMarker : Marker
    {
        public override string Identifier => "em";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() { typeof(TextBlock) };

    }
}
