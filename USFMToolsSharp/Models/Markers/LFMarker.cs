using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List footer
    /// </summary>
    /// <remarks>See https://ubsicap.github.io/usfm/lists/index.html#lf</remarks>
    public class LFMarker : Marker
    {
        public override string Identifier => "lf";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
