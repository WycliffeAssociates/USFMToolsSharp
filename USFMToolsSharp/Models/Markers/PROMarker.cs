using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Name of God (name of Deity)
    /// </summary>
    public class PROMarker : Marker
    {
        public override string Identifier => "pro";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
