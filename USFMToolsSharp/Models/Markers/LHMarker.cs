using System;
using System.Collections.Generic;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List header
    /// </summary>
    /// <remarks>See https://ubsicap.github.io/usfm/lists/index.html#lh</remarks>
    public class LHMarker : Marker
    {
        public override string Identifier => "lh";

        public override List<Type> AllowedContents => new()
        {
            typeof(TextBlock)
        };
    }
}