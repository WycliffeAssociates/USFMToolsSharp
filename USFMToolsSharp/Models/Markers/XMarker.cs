using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Cross reference element
    /// </summary>
    public class XMarker : Marker
    {
        public override string Identifier => "x";
        public string CrossRefCaller;

        public override string PreProcess(string input)
        {
            CrossRefCaller = input.Trim();
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(XOMarker),
            typeof(XTMarker),
            typeof(XQMarker),
            typeof(TextBlock),
        };
    }
}
