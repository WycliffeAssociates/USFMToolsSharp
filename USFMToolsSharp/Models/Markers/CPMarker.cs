using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{

    /// <summary>
    /// Published Chapter Marker (for when it isn't an english number)
    /// </summary>
    public class CPMarker : Marker
    {
        public string PublishedChapterMarker;
        public override string Identifier => "cp";
        public override void Populate(string input)
        {
            PublishedChapterMarker = input;
        }
    }
}
