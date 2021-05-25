using System.Collections.Generic;
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

        public override string PreProcess(string input)
        {
            input = input.Trim();

            string[] wordEntry = input.Split('|');
            if (wordEntry.Length > 0)
            {
                Caption = wordEntry[0];
            }
            if (wordEntry.Length > 1)
            {
                Description = wordEntry[1];
            }
            if (wordEntry.Length > 2)
            {
                Width = wordEntry[2];
            }
            if (wordEntry.Length > 3)
            {
                Location = wordEntry[3];
            }
            if (wordEntry.Length > 4)
            {
                Copyright = wordEntry[4];
            }
            if (wordEntry.Length > 5)
            {
                Reference = wordEntry[5];
            }
            if (wordEntry.Length > 6)
            {
                FilePath = wordEntry[6];
            }


            return string.Empty;
        }

    }
}
