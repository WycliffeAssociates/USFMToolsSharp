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
        private ILinterModule[] linters = { };
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
