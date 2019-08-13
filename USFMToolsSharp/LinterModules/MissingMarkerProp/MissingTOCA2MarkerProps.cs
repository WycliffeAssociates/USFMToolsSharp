using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingTOCA2MarkerProps : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> results = new List<LinterResult>();
            foreach(Marker marker in input.GetChildMarkers<TOCA2Marker>())
            {
                if (String.IsNullOrEmpty(((TOCA2Marker)marker).AltShortTableOfContentsText))
                {
                    results.Add(new LinterResult
                    {
                        Position = marker.Position,
                        Level = LinterLevel.Error,
                        Message = "Short Table of Contents Text is missing"
                    });
                }
            }
            return results;
        }
        
    }
}