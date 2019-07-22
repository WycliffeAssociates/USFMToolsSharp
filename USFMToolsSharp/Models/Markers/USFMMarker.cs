using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Marker for USFM version
    /// </summary>
    public class USFMMarker : Marker
    {
        public override string Identifier => "usfm";

        /// <summary>
        /// USFM Version
        /// </summary>
        public string Version { get; set; }

        public override string PreProcess(string input)
        {
            Version = input;
            return string.Empty;
        }
    }
}
