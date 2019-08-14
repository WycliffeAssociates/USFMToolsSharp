using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class MissingTableRows : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> output = new List<LinterResult>();

            foreach (Marker marker in input.GetChildMarkers<TCMarker>())
            {
                LinterResult tmp = ValidateTable(marker, input);
                if (tmp != null)
                {
                    output.Add(tmp);
                }
            }
            foreach (Marker marker in input.GetChildMarkers<TCRMarker>())
            {
                LinterResult tmp = ValidateTable(marker, input);
                if (tmp != null)
                {
                    output.Add(tmp);
                }
            }
            foreach (Marker marker in input.GetChildMarkers<THMarker>())
            {
                LinterResult tmp = ValidateTable(marker, input);
                if (tmp != null)
                {
                    output.Add(tmp);
                }
            }
            foreach (Marker marker in input.GetChildMarkers<THRMarker>())
            {
                LinterResult tmp = ValidateTable(marker, input);
                if (tmp != null)
                {
                    output.Add(tmp);
                }
            }
            return output;
        }
        /// <summary>
        /// Checks if Table Cells/Headers are contained within a Table Row
        /// </summary>
        /// <param name="input"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private LinterResult ValidateTable(Marker input,USFMDocument root)
        {
            List<Marker> hierarchy = root.GetHierarchyToMarker(input);
            Marker parentMarker = hierarchy[hierarchy.Count - 2];

            if(!(parentMarker is TRMarker))
            {
                return new LinterResult
                {
                    Position = input.Position,
                    Level = LinterLevel.Warning,
                    Message = $"Missing Table Row container"
                };
            }
            return null;

        }
    }
    
}
