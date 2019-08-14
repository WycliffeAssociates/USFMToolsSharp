using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class VerseMarkerValidation : ILinterModule
    {
        private static Regex ValidRegex = new Regex("^[0-9\\-]+$");
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> output = new List<LinterResult>();
            LinterResult tmp;
            foreach(var verse in input.GetChildMarkers<VMarker>())
            {
                tmp = ValidateVerse(verse);
                if (tmp != null)
                {
                    output.Add(tmp);
                }
            }

            return output;
        }

        private LinterResult ValidateVerse(VMarker input)
        {
            if (string.IsNullOrWhiteSpace(input.VerseNumber))
            {
                return new LinterResult(LinterLevel.Error, $"Verse number is missing", input.Position);
            }

            if (!ValidRegex.IsMatch(input.VerseNumber))
            {
                return new LinterResult(LinterLevel.Error, $"Verse number is invalid", input.Position);
            }

            return null;
        }
    }
}
