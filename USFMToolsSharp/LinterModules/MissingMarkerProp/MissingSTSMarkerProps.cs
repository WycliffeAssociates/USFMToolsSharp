using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingSTSMarkerProps : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> results = new List<LinterResult>();
            foreach(Marker marker in input.GetChildMarkers<STSMarker>())
            {
                if (String.IsNullOrEmpty(((STSMarker)marker).StatusText))
                {
                    results.Add(new LinterResult
                    {
                        Position = marker.Position,
                        Level = LinterLevel.Error,
                        Message = "Status Information is missing"
                    });
                }
            }
            return results;
        }
        
    }
}