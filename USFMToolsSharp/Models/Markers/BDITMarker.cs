using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Bold-italic text
    /// </summary>
    public class BDITMarker : Marker
    {
        public override string Identifier => "bdit";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        private static HashSet<Type> AllowedContentsStatic => new () { typeof(TextBlock) };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;
    }
}
