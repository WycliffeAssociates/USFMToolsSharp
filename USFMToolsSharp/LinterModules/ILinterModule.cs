using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public interface ILinterModule
    {
        List<LinterResult> Lint(USFMDocument input);
    }
}
