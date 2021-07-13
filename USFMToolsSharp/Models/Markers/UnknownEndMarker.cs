using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class UnknownEndMarker : Marker
    {
        public string ParsedIdentifier;
        public override string Identifier => "unk*";
    }

}
