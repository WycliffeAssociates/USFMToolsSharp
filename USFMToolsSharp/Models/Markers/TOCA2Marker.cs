using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for the Alternative language short table of contents
    /// </summary>
    public class TOCA2Marker : Marker
    {
        public string AltShortTableOfContentsText;
        public override string Identifier => "toca2";
        public override string PreProcess(string input)
        {
            AltShortTableOfContentsText = input;
            return string.Empty;
        }
    }
}
