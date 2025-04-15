using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Models
{
    /// <summary>
    /// A holder class to take the place of a tuple
    /// </summary>
    ref struct ConvertToMarkerResult
    {
        public ConvertToMarkerResult(Marker marker, ReadOnlySpan<char> remainingText)
        {
            this.marker = marker;
            this.remainingText = remainingText;
        }
        public Marker marker { get; set; }
        public ReadOnlySpan<char> remainingText { get; set; }
    }
}
