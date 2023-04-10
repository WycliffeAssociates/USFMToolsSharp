using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List total end marker
    /// </summary>
    /// <remarks>See https://ubsicap.github.io/usfm/lists/index.html#litl-litl</remarks>
    public class LITLEndMarker : Marker
    {
        public override string Identifier => "litl*";
    }
}
