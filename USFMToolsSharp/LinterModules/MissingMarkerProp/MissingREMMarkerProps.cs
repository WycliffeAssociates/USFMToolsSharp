using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingREMMarkerProps : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> results = new List<LinterResult>();
            foreach(Marker marker in input.GetChildMarkers<REMMarker>())
            {
                if (String.IsNullOrEmpty(((REMMarker)marker).Comment))
                {
                    results.Add(new LinterResult
                    {
                        Position = marker.Position,
                        Level = LinterLevel.Error,
                        Message = "Remark Comment is missing"
                    });
                }
            }
            return results;
        }
        
    }
}