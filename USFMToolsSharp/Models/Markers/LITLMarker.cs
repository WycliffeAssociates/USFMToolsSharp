using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List total
    /// </summary>
    /// <remarks>See https://ubsicap.github.io/usfm/lists/index.html#litl-litl</remarks>
    public class LITLMarker : Marker
    {
        public override string Identifier => "litl";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
