using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// List Entry Marker within Introduction
    /// </summary>
    public class ILIMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "ili";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.Trim();
        }
        public override HashSet<Type> AllowedContents => new () {
            typeof(TextBlock)
        };
    }
}
