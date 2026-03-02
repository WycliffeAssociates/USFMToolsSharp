using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public abstract class Marker
    {
        public Marker()
        {
            
        }
        public abstract string Identifier { get; }
        public int Position { get; set; }
        public HierarchyNode? DefaultHierarchyNode { get; set; }

        /// <summary>
        /// Pre-process the text contents before creating text elements inside of it
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input;
        }

    }
}
