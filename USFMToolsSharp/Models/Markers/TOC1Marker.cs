using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A tag for the long table of contents
    /// </summary>
    public class TOC1Marker : Marker
    {
        public string LongTableOfContentsText;
        public override string Identifier => "toc1";
        public override void Populate(string input)
        {
            LongTableOfContentsText = input;
        }
    }
}
