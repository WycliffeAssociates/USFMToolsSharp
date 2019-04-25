﻿using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for book abbreviation
    /// </summary>
    public class TOC3Marker : Marker
    {
        public string BookAbbreviation;
        public override string Identifier => "toc3";
        public override string PreProcess(string input)
        {
            BookAbbreviation = input;
            return string.Empty;
        }
    }
}
