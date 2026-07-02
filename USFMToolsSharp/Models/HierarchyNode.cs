using System;
using System.Collections.Generic;
using System.Linq;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Models;

public class HierarchyNode
{
    public HierarchyNode(Marker? marker)
    {
        Marker = marker;
        MarkerType = marker?.GetType();
    }
    public Marker? Marker { get;  }
    public Type? MarkerType { get; }
    public T As<T>() where T : Marker
    {
        return (T)Marker!;
    }
    public List<HierarchyNode> Contents { get; set; } = new List<HierarchyNode>();
    
    public HierarchyNode this[int index]
    {
        get => Contents[index];
        set => Contents[index] = value;
    }
    
    public static implicit operator Marker?(HierarchyNode input) => input.Marker;
    /// <summary>
    /// A recursive search for children of a certain type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<HierarchyNode> GetChildMarkers<T>(List<Type>? ignoredParents = null) where T : Marker
    {
        var output = new List<HierarchyNode>();
        var stack = new Stack<HierarchyNode>(Contents.Count);

        if (ignoredParents != null && ignoredParents.Contains(this.MarkerType))
        {
            return output;
        }

        stack.Push(this);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (node.Marker is T)
            {
                output.Add(node);
            }

            for (var index = node.Contents.Count - 1; index >= 0; index--)
            {
                var child = node.Contents[index];
                if (ignoredParents == null || !ignoredParents.Contains(child.MarkerType))
                {
                    stack.Push(child);
                }
            }
        }

        return output;
    }
    public List<Marker> GetHierarchyToMarker(Marker target)
    {
        if (this.Contents.Count == 0)
        {
            return [];
        }
        var parents = new Stack<(HierarchyNode node, bool isLastInParent)>();
        int childMarkerContentsCount;

        bool found = false;
        var stack = new Stack<(HierarchyNode node, bool isLastInParent)>();
        stack.Push((this, false));
        while (stack.Count > 0)
        {
            var (marker, isLastInParent) = stack.Pop();
            if (marker.Marker == target)
            {
                found = true;
                break;
            }

            if (marker.Contents.Count != 0)
            {
                // We're descending
                parents.Push((marker, isLastInParent));

                childMarkerContentsCount = marker.Contents.Count;
                for (int i = 0; i < childMarkerContentsCount; i++)
                {
                    stack.Push((marker.Contents[i], i == 0));
                }
            }
            else if (stack.Count == 0 || isLastInParent)
            {
                // We're ascending
                var tmp = parents.Pop();
                // keep moving up the parent stack until we aren't the last in a parent
                while(tmp.isLastInParent)
                {
                    tmp = parents.Pop();
                }
            }
        }

        if (!found)
        {
            return [];
        }
        var output = new List<Marker>(parents.Count + 1) { target };

        output.AddRange(parents.Select(i => i.node.Marker));
        output.Reverse();
        return output;
    }
        /// <summary>
        /// Get the paths to multiple markers
        /// </summary>
        /// <param name="targets">A list of markers to find</param>
        /// <returns>A dictionary of markers and paths</returns>
        /// <remarks>In the case that the marker doesn't exist in the tree the dictionary will contain an empty list for that marker</remarks>
        public Dictionary<Marker, List<Marker>> GetHierarchyToMultipleMarkers(List<Marker> targets)
        {
            if (targets.Count == 0)
            {
                return new Dictionary<Marker, List<Marker>>();
            }
            if (Contents.Count == 0)
            {
                return targets.ToDictionary(i => i, i => new List<Marker>());
            }

            // Create a set for fast lookup
            var targetSet = new HashSet<Marker>(targets);
            
            var output = new Dictionary<Marker, List<Marker>>(targets.Count);
            var parents = new Stack<(HierarchyNode marker, bool isLastInParent)>();

            var stack = new Stack<(HierarchyNode marker, bool isLastInParent)>();
            stack.Push((this, false));
            while (stack.Count > 0)
            {
                var (node, isLastInParent) = stack.Pop();
                if (node.Marker != null && targetSet.Contains(node.Marker))
                {
                    var tmp = new List<Marker>(parents.Count + 1)
                    {
                        node.Marker
                    };
                    tmp.AddRange(parents.Select(i=> i.marker.Marker));
                    tmp.Reverse();
                    output.Add(node.Marker, tmp);
                    if (output.Count == targets.Count)
                    {
                        break;
                    }
                }

                if (node.Contents.Count != 0)
                {
                    // We're descending
                    parents.Push((node, isLastInParent));

                    var childMarkerContentsCount = node.Contents.Count;
                    for (var i = 0; i < childMarkerContentsCount; i++)
                    {
                        stack.Push((node.Contents[i], i == 0));
                    }
                }
                else if (stack.Count == 0 || isLastInParent)
                {
                    // We're ascending
                    var tmp = parents.Pop();
                    // keep moving up the parent stack until we aren't the last in a parent
                    while(tmp.isLastInParent)
                    {
                        tmp = parents.Pop();
                    }
                }
            }

            foreach (var i in targets)
            {
                output.TryAdd(i, []);
            }

            return output;
        }
        /// <summary>
        /// Get the types path to the last marker in the hierarchy
        /// </summary>
        /// <returns>A list of types from the root to the last marker</returns>
        public List<Type> GetTypesPathToLastMarker() 
        {
            var types = new List<Type> { this.MarkerType };
            var current = this;
            while (current.Contents.Count > 0)
            {
                current = current.Contents[^1];
                types.Add(current.MarkerType ?? typeof(USFMDocument));
            }
            return types;
        }


        /// <summary>
        /// Get the last descendent marker in the hierarchy
        /// </summary>
        /// <returns>The marker at the very end</returns>
        public Marker GetLastDescendent()
        {
            var current = this;
            while (current.Contents.Count > 0)
            {
                current = current.Contents[^1];
            }
            return current.Marker!;
        }
        public List<HierarchyNode> GetNodesToMarker(Marker target)
        {
            if (this.Contents.Count == 0)
            {
                return [];
            }
            
            var parents = new Stack<(HierarchyNode node, bool isLastInParent)>();
            int childMarkerContentsCount;

            bool found = false;
            var stack = new Stack<(HierarchyNode node, bool isLastInParent)>();
            stack.Push((this, false));
            HierarchyNode? foundNode = null;
            while (stack.Count > 0)
            {
                var (marker, isLastInParent) = stack.Pop();
                if (marker.Marker == target)
                {
                    found = true;
                    foundNode = marker;
                    break;
                }

                if (marker.Contents.Count != 0)
                {
                    // We're descending
                    parents.Push((marker, isLastInParent));

                    childMarkerContentsCount = marker.Contents.Count;
                    for (int i = 0; i < childMarkerContentsCount; i++)
                    {
                        stack.Push((marker.Contents[i], i == 0));
                    }
                }
                else if (stack.Count == 0 || isLastInParent)
                {
                    // We're ascending
                    var tmp = parents.Pop();
                    // keep moving up the parent stack until we aren't the last in a parent
                    while(tmp.isLastInParent)
                    {
                        tmp = parents.Pop();
                    }
                }
            }

            if (!found)
            {
                return [];
            }
            var output = new List<HierarchyNode>(parents.Count + 1) { foundNode! };

            output.AddRange(parents.Select(i => i.node));
            output.Reverse();
            return output;
        }
}