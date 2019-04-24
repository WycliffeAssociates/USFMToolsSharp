using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Bold Marker
    /// </summary>
    public class BDMarker : Marker
    {
        /// <summary>
        /// Text that is bolded
        /// </summary>
        public string Text;
        public override string Identifier => "bd";
        public override void Populate(string input)
        {
            Text = input;
        }
    }
}
