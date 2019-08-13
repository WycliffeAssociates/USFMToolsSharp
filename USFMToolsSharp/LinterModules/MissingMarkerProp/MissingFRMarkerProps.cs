using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingFRMarkerProps : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> results = new List<LinterResult>();
            foreach(Marker marker in input.GetChildMarkers<FRMarker>())
            {
                if (String.IsNullOrEmpty(((FRMarker)marker).VerseReference))
                {
                    results.Add(new LinterResult
                    {
                        Position = marker.Position,
                        Level = LinterLevel.Error,
                        Message = "Footnote Verse Reference is missing"
                    });
                }
            }
            return results;
        }
        
    }
}