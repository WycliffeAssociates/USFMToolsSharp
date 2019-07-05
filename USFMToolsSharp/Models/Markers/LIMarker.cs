using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List Entry Marker
    /// </summary>
    public class LIMarker : Marker
    {
        public int Depth = 1;
        public string Text;
        public override string Identifier => "li";
        public override string PreProcess(string input)
        {
            return string.Empty;
        }
        public override List<Type> AllowedContents
        {
            get
            {
                return new List<Type>()
                {
                    typeof(TextBlock)
                };
            }
        }
    }
}
