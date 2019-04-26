﻿using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Acrostic heading for poetry
    /// </summary>
    public class QAMarker : Marker
    {
        /// <summary>
        /// Heading for the poetry
        /// </summary>
        public string Heading;
        public override string Identifier => "qa";
        public override string PreProcess(string input)
        {
            Heading = input;
            return string.Empty;
        }
    }
}
