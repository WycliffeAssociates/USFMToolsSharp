﻿using System;

 namespace USFMToolsSharp.Models.Markers
{
    public class SLSMarker : Marker
    {
        public override string Identifier => "sls";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
    }
}
