using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Poetry Selah Marker (I know weird but it is in the spec)
    /// </summary>
    public class QSMarker : Marker
    {
        public String Text;
        public override string Identifier => "qs";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
