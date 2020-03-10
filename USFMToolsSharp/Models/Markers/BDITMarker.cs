using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Bold-italic text
    /// </summary>
    public class BDITMarker : Marker
    {
        public override string Identifier => "bdit";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() { typeof(TextBlock) };
    }
}
