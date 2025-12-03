using System;
using System.Collections.Generic;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Models;

public class HierarchyDefinition
{
    public HierarchyDefinition()
    {
        
    }

    public HierarchyDefinition(Type[] allowedTypes)
    {
        AllowedChildren = new HashSet<Type>(allowedTypes);
    }
    public HashSet<Type> AllowedChildren { get; set; } = new HashSet<Type>();
    public Func<Type, HierachyNode, Marker, bool>? CanInsert = null;
    
}