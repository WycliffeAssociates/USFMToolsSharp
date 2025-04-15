using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Emphasis text
    /// </summary>
    public class EMMarker : Marker
    {
        public override string Identifier => "em";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        private static HashSet<Type> AllowedContentsStatic => new () { typeof(TextBlock) };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;

    }
}
