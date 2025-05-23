using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Wordlist / Glossary / Dictionary Entry Marker
    /// </summary>
    public partial class WMarker : Marker
    {
        public string Term;
        public Dictionary<string, string> Attributes;
        public override string Identifier => "w";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            input = input.Trim();
            Attributes = new Dictionary<string, string>();

            string[] wordEntry = input.ToString().Split('|');
            Term = wordEntry[0];

            if (wordEntry.Length > 1)
            {
                string[] wordAttr = wordEntry[1].Split(' ');
                foreach (string attr in wordAttr)
                {
                    Match attrMatch = GetWordRegex().Match(attr);
                    if (attrMatch.Groups[2].ValueSpan.Length == 0)
                    {
                        Attributes["lemma"] = attrMatch.Groups[1].Value;
                    }
                    else
                    {
                        Attributes[attrMatch.Groups[1].Value] = attrMatch.Groups[2].Value;
                    }

                }

            }

            return ReadOnlySpan<char>.Empty;
        }

        [GeneratedRegex("([\\w-]+)=?\"?([\\w,:.]*)\"?", RegexOptions.Compiled | RegexOptions.Singleline)]
        private static partial Regex GetWordRegex();
    }
}
