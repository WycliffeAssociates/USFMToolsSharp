using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Tag for alternative language book abbreviation
    /// </summary>
    public class TOCA3Marker : Marker
    {
        public string AltBookAbbreviation;
        public override string Identifier => "toca3";
        public override string PreProcess(string input)
        {
            AltBookAbbreviation = input;
            return string.Empty;
        }
    }
}
