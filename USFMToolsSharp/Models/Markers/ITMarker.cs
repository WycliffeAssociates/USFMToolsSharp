using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Italic marker
    /// </summary>
    public class ITMarker : Marker
    {
        public override string Identifier => "it";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() { typeof(TextBlock) };
    }
}
