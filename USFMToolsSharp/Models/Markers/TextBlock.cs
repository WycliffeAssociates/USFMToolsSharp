using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A "marker" for a text block. This exists so that we can handle 
    /// </summary>
    public class TextBlock : Marker
    {
        public TextBlock(string text)
        {
            Text = text;
        }
        public string Text;
        public override string Identifier => string.Empty;
    }
}
