using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Wordlist / Glossary / Dictionary Entry Marker
    /// </summary>
    public class WMarker : Marker
    {
        public string Term;
        public Dictionary<string, string> attributes;
        public override string Identifier => "w";

        public override string PreProcess(string input)
        {
            Regex pattern = new Regex("(.*)[|](.*)");
            Match match = pattern.Match(input);
            Term = match.Groups[1].Value;

            Regex wordAttr = new Regex();

            return base.PreProcess(input);
        }

    }
}
