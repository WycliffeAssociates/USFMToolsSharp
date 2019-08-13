using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingQAMarkerProps : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> results = new List<LinterResult>();
            foreach(Marker marker in input.GetChildMarkers<QAMarker>())
            {
                if (String.IsNullOrEmpty(((QAMarker)marker).Heading))
                {
                    results.Add(new LinterResult
                    {
                        Position = marker.Position,
                        Level = LinterLevel.Error,
                        Message = "Acrositic Heading is missing"
                    });
                }
            }
            return results;
        }
        
    }
}