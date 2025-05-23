﻿using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Major title marker
    /// </summary>
    public class MTMarker : Marker
    {
        public int Weight = 1;
        public string Title;
        public override string Identifier => "mt";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            Title = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
