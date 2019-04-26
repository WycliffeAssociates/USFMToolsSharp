using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Models
{
    /// <summary>
    /// A holder class to take the place of a tuple
    /// </summary>
    class ConvertToMarkerResult
    {
        public ConvertToMarkerResult(Marker marker, string remainingText)
        {
            this.marker = marker;
            this.remainingText = remainingText;
        }
        public Marker marker;
        public string remainingText;
    }
}
