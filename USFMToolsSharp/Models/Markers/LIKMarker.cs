﻿using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class LIKMarker : Marker
    {
        public override string Identifier => "lik";
        public override string PreProcess(string input)
        {
            return input.Trim();
        }
        public override List<Type> AllowedContents => new List<Type>() {
            typeof(TextBlock)
        };
    }
}
