using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingTOCA3MarkerProps : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> results = new List<LinterResult>();
            foreach(Marker marker in input.GetChildMarkers<TOCA3Marker>())
            {
                if (String.IsNullOrEmpty(((TOCA3Marker)marker).AltBookAbbreviation))
                {
                    results.Add(new LinterResult
                    {
                        Position = marker.Position,
                        Level = LinterLevel.Error,
                        Message = "Table of Contnets (Alternate Book Abbreviation) is missing"
                    });
                }
            }
            return results;
        }
        
    }
}