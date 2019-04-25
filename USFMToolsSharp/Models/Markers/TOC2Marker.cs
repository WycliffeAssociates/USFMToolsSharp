using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for the short table of contents
    /// </summary>
    public class TOC2Marker : Marker
    {
        public string ShortTableOfContentsText;
        public override string Identifier => "toc2";
        public override string PreProcess(string input)
        {
            ShortTableOfContentsText = input;
            return string.Empty;
        }
    }
}
