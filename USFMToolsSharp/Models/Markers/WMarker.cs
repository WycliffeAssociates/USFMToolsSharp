using System;
using System.Collections.Generic;
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
            var wordEntryEnumerator = input.Split('|');
            wordEntryEnumerator.MoveNext();
            Term = input[wordEntryEnumerator.Current].ToString();

            // If we have another bit after the |
            if (wordEntryEnumerator.MoveNext())
            {
                foreach (var attrRange in input[wordEntryEnumerator.Current].Split(' '))
                {
                    var attr = input[(attrRange.Start.Value + wordEntryEnumerator.Current.Start.Value) .. (attrRange.End.Value + wordEntryEnumerator.Current.Start.Value)];
                    var eq = attr.IndexOf('=');
                    if (eq != -1)
                    {
                        var key = attr[..eq];
                        var value = attr[(eq + 1)..];
                        Attributes[key.ToString()] = value.Trim('"').ToString();
                    }
                    else
                    {
                        Attributes["lemma"] = attr.ToString();
                    }
                }

            }

            return ReadOnlySpan<char>.Empty;
        }
    }
}
