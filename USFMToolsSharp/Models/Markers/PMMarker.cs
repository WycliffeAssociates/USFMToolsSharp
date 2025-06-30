using System;
using System.Collections.Generic;

namespace USFMToolsSharp.Models.Markers;

public class PMMarker: Marker
{
    public override string Identifier { get; } = "pm";
    public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
    {
        return input.TrimStart();
    }

    public override HashSet<Type> AllowedContents => AllowedContentsStatic;

    private static HashSet<Type> AllowedContentsStatic { get; } = new() {
        typeof(VMarker),
        typeof(BMarker),
        typeof(SPMarker),
        typeof(TextBlock),
        typeof(FMarker),
        typeof(FEndMarker),
        typeof(LIMarker),
        typeof(QMarker),
        typeof(XMarker),
        typeof(SCMarker),
    };
}