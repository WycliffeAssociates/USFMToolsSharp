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

        public override string Identifier => "";

        public override List<Type> AllowedContents
        {
            get
            {
                return new List<Type>(){
                    typeof(HMarker),
                    typeof(IDEMarker),
                    typeof(IDMarker),
                    typeof(STSMarker),
                    typeof(USFMMarker),
                    typeof(TOC1Marker),
                    typeof(TOC2Marker),
                    typeof(TOC3Marker),
                    typeof(TOCA1Marker),
                    typeof(TOCA2Marker),
                    typeof(TOCA3Marker),
                    typeof(ISMarker),
                    typeof(MTMarker),
                    typeof(IMTMarker),
                    typeof(CLMarker),
                    typeof(CMarker)
                };
            }
        }
        public void Insert(Marker input)
        {
            if (!TryInsert(input))
            {
                // Since this is the root then add them anyway
                Contents.Add(input);
            }

        }
        public void Insert(USFMDocument document)
        {
            InsertMultiple(document.Contents);
        }
        public void InsertMultiple(IEnumerable<Marker> input)
        {
            foreach(Marker i in input)
            {
                Insert(i);
            }

        }
    }
}
