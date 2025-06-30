using System;
using System.Collections.Generic;

namespace USFMToolsSharp.Models.Markers;

public class FLMarker: Marker
{
    public override string Identifier { get; } = "fl";
    private static HashSet<Type> AllowedContentsStatic { get; } = new ()
    {
        typeof(TextBlock),
    };
    public override HashSet<Type> AllowedContents => AllowedContentsStatic;
}