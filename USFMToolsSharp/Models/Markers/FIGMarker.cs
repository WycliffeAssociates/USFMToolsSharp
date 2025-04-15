using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace USFMToolsSharp.Models.Markers
{

    /// <summary>
    /// Wordlist / Glossary / Dictionary Entry Marker
    /// </summary>
    public class FIGMarker : Marker
    {
        public string Caption;
        public string Description;
        public string Width;
        public string Location;
        public string Copyright;
        public string Reference;
        public string FilePath;

        public override string Identifier => "fig";

        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            // TODO: This can probably be done with a span so we can come back to it later
            input = input.Trim();
            var split = input.ToString().Split('|');

            string[] wordEntry = split;
            if (wordEntry.Length > 0)
            {
                Description = wordEntry[0].Trim();
            }
            if (wordEntry.Length > 1)
            {
                FilePath = wordEntry[1].Trim();
            }
            if (wordEntry.Length > 2)
            {
                Width = wordEntry[2].Trim();
            }
            if (wordEntry.Length > 3)
            {
                Location = wordEntry[3].Trim();
            }
            if (wordEntry.Length > 4)
            {
                Copyright = wordEntry[4].Trim();
            }
            if (wordEntry.Length > 5)
            {
               Caption = wordEntry[5].Trim();
            }
            if (wordEntry.Length > 6)
            {
                Reference = wordEntry[6].Trim();
            }


            
            string[] contentArr = split;
            if (contentArr.Length > 0 && contentArr.Length <= 2)
            {
                Caption = contentArr[0].Trim();

                string[] attributes = contentArr[1].Split('"');
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i].Replace(" ", "").Contains("alt="))
                    {
                        Description = attributes[i + 1].Trim();
                    }
                    if (attributes[i].Replace(" ", "").Contains("src="))
                    {
                        FilePath = attributes[i + 1].Trim();
                    }
                    if (attributes[i].Replace(" ", "").Contains("size="))
                    {
                        Width = attributes[i + 1].Trim();
                    }
                    if (attributes[i].Replace(" ", "").Contains("loc="))
                    {
                        Location = attributes[i + 1].Trim();
                    }
                    if (attributes[i].Replace(" ", "").Contains("copy="))
                    {
                        Copyright = attributes[i + 1].Trim();
                    }
                    if (attributes[i].Replace(" ", "").Contains("ref="))
                    {
                        Reference = attributes[i + 1].Trim();
                    }
                }
            }

            

            return string.Empty;
            
        }

    }
}
