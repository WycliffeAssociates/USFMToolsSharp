using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class PNMarker : Marker
    {
        public override string Identifier => "pn";

        public override List<Type> AllowedContents => new List<Type>() { typeof(TextBlock) };
    }
}
