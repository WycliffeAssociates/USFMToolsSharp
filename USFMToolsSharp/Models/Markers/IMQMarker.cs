using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction flush left (margin) quote from text paragraph
    /// </summary>
    public class IMQMarker : Marker
    {
        public override string Identifier => "imq";
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(TextBlock),
            typeof(BKMarker),
            typeof(BKEndMarker),
            typeof(BDMarker),
            typeof(BDEndMarker),
            typeof(ITMarker),
            typeof(ITEndMarker)

        };

    }
}
