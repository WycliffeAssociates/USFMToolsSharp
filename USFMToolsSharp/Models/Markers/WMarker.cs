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
        public Dictionary<string, string> Attributes;
        private static Regex wordAttrPattern = new Regex("([\\w]+)=?\"?([\\w,:.]*)\"?", RegexOptions.Singleline);
        public override string Identifier => "w";

        public override string PreProcess(string input)
        {
            input = input.Trim();
            Attributes = new Dictionary<string, string>();

            string[] wordEntry = input.Split('|');
            Term = wordEntry[0];

            if (wordEntry.Length > 1)
            {

                string[] wordAttr = wordEntry[1].Split(' ');
                foreach (string attr in wordAttr)
                {
                    Match attrMatch = wordAttrPattern.Match(attr);
                    if (attrMatch.Groups[2].Value.Length == 0)
                    {
                        Attributes["lemma"] = attrMatch.Groups[1].Value;
                    }
                    else
                    {
                        Attributes[attrMatch.Groups[1].Value] = attrMatch.Groups[2].Value;
                    }

                }

            }

            return string.Empty;
        }

    }
}
