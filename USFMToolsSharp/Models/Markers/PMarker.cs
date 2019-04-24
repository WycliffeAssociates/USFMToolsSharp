using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class PMarker : Marker
    {
        public override string Identifier => "p";
        public override List<Type> AllowedContents => new List<Type>() { typeof(VMarker)};
    }
}
