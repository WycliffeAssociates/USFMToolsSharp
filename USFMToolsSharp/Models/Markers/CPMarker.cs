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
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            PublishedChapterMarker = input.Trim().ToString();
            return ReadOnlySpan<char>.Empty;
        }
    }
}
