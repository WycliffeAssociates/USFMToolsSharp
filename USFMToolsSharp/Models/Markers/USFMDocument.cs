﻿using System;
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

        public override string Identifier => string.Empty;
        
        public int NumberOfTotalMarkersAtParse { get; set; }

        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
        private static HashSet<Type> AllowedContentsStatic { get; } =
            new(){
                typeof(HMarker),
                typeof(IDEMarker),
                typeof(IDMarker),
                typeof(IBMarker),
                typeof(IQMarker),
                typeof(ILIMarker),
                typeof(IOTMarker),
                typeof(IOMarker),
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
                typeof(IPMarker),
                typeof(IPIMarker),
                typeof(IMMarker),
                typeof(IMIMarker),
                typeof(IPQMarker),
                typeof(IMQMarker),
                typeof(IPRMarker),
                typeof(CLMarker),
                typeof(CMarker)
            };

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
