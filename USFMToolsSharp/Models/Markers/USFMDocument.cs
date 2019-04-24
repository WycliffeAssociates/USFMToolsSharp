using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class USFMDocument: Marker
    {
        public USFMDocument()
        {
            Contents = new List<Marker>();
        }

        public override string Identifier => throw new NotImplementedException();

        public override List<Type> AllowedContents
        {
            get
            {
                return new List<Type>(){
                    typeof(HMarker),
                    typeof(IDEMarker),
                    typeof(IDMarker),
                    typeof(TOC1Marker),
                    typeof(TOC2Marker),
                    typeof(TOC3Marker),
                    typeof(MTMarker),
                    typeof(CMarker),
                    typeof(CLMarker),
                };
            }
        }
    }
}
