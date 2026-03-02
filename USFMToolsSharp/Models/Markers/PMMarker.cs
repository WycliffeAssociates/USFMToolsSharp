using System;

namespace USFMToolsSharp.Models.Markers;

public class PMMarker: Marker
{
    public override string Identifier { get; } = "pm";
    public override ReadOnlySpan<char> PreProcess(ReadOnlySpan<char> input)
    {
        return input.TrimStart();
    }
}