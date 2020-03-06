using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// An end marker for FQA
    /// </summary>
    public class FQAEndMarker : Marker
    {
        public override string Identifier => "fqa*";
    }
}
