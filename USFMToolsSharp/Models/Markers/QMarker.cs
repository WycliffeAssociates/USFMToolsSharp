using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A Poetry Marker
    /// </summary>
    public class QMarker : Marker
    {
        public int Depth = 1;
        public string Text;
        public override string Identifier => "q";
        public override string PreProcess(string input)
        {
            return input.TrimStart();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(BMarker),
            typeof(QSMarker),
            typeof(QSEndMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
            typeof(VMarker),
        };

        public override bool TryInsert(Marker input)
        {
            if (input is VMarker && Contents.Any(m => m is VMarker))
            {
                return false;
            }

            return base.TryInsert(input);
        }
    }
}
