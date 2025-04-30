using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// A Poetry Marker
    /// </summary>
    public class QMarker : Marker
    {
        public int Depth = 1;
        public string Text;
        public bool IsPoetryBlock;
        public override string Identifier => "q";
        public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
        {
            return input.TrimStart();
        }
        private static HashSet<Type> AllowedContentsStatic { get; } = new() {
            typeof(BMarker),
            typeof(QSMarker),
            typeof(QSEndMarker),
            typeof(TextBlock),
            typeof(FMarker),
            typeof(FEndMarker),
            typeof(TLMarker),
            typeof(TLEndMarker),
            typeof(WMarker),
            typeof(WEndMarker),
            typeof(VMarker),
        };
        public override HashSet<Type> AllowedContents => AllowedContentsStatic;

        public override bool TryInsert(Marker input, Type markerType = null)
        {
            if (input is VMarker && Contents.Any(m => m is VMarker))
            {
                return false;
            }

            return base.TryInsert(input, markerType);
        }
    }
}
