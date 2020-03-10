using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A tag for the alternative language long table of contents
    /// </summary>
    public class TOCA1Marker : Marker
    {
        public string AltLongTableOfContentsText;
        public override string Identifier => "toca1";
        public override string PreProcess(string input)
        {
            AltLongTableOfContentsText = input.Trim();
            return string.Empty;
        }
    }
}
