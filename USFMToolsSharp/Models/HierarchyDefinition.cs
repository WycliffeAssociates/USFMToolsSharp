using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Models;

public class HierarchyDefinition
{
    public HierarchyDefinition()
    {
        AllowedChildren = FrozenSet<Type>.Empty;
    }

    public HierarchyDefinition(Type[] allowedTypes)
    {
        AllowedChildren = new HashSet<Type>(allowedTypes).ToFrozenSet();
    }
    public FrozenSet<Type> AllowedChildren { get; set; }
    public Func<Type, HierarchyNode, Marker, bool>? CanInsert = null;
    
}