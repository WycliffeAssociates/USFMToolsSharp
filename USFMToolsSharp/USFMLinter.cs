using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.LinterModules;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public class USFMLinter
    {
        public List<ILinterModule> linters = new List<ILinterModule>() {
            new FindUnknownMarkers(),
            new VerseMarkerValidation(),
            new MissingEndMarkers(),
            new UnpairedEndMarkers()
        };
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> output = new List<LinterResult>();
            foreach(var linter in linters)
            {
                output.AddRange(linter.Lint(input));
            }

            return output;
        }
    }
}
