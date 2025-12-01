using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class USFMDocument
    {
        public USFMDocument()
        {
        }

        public List<HierachyNode> Hierachies { get; set; } = new List<HierachyNode>();
        
        public int NumberOfTotalMarkersAtParse { get; set; }

        public void Insert(Marker input, List<Dictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            var markerType = input.GetType();
            for (var i = 0; i < hierarchyDefinitions.Count; i++)
            {
                if (Hierachies[i].Contents.Count == 0)
                {
                    Hierachies[i].Contents.Add(new HierachyNode(input));
                    return;
                }
                var stack = new Stack<HierachyNode>();
                var toLastChildStack = new Stack<HierachyNode>();
                toLastChildStack.Push(Hierachies[i].Contents[^1]);
                stack.Push(Hierachies[i].Contents[^1]);
                while (toLastChildStack.Count > 0)
                {
                    var currentNode = toLastChildStack.Pop();
                    stack.Push(currentNode);
                    if (currentNode.Contents.Count == 0)
                    {
                        continue;
                    }
                    toLastChildStack.Push(currentNode.Contents[^1]);
                }
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    var currentNodeType = currentNode.Marker.GetType();
                    var currentNodeDefinition = hierarchyDefinitions[i].GetValueOrDefault(currentNodeType);
                    if (currentNodeDefinition == null)
                    {
                        continue;
                    }
                    if (!currentNodeDefinition.AllowedChildren.Contains(markerType))
                    {
                        continue;
                    }
                    if (currentNodeDefinition.CanInsert != null && !currentNodeDefinition.CanInsert(currentNodeType, input))
                    {
                        continue;
                    }
                    currentNode.Contents.Add(new HierachyNode(input));
                    break;
                }
            }
        }
        public List<Type> GetTypesPathToLastMarker()
        {
            List<Type> types = new List<Type>();
            if (Hierachies.Count == 0)
            {
                return types;
            }
            types.Add(typeof(USFMDocument));
            var currentNode = Hierachies[^1];
            types.Add(currentNode.Marker.GetType());
            while (currentNode.Contents.Count > 0)
            {
                currentNode = currentNode.Contents[^1];
                types.Add(currentNode.Marker.GetType());
            }
            return types;
        }
        
        // Backwards compatibility
        [Obsolete("Use Hierachies[0].Contents instead")]
        public List<HierachyNode> Contents => Hierachies[0].Contents;
        
        public void InsertMultiple(IEnumerable<Marker> input, List<Dictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            foreach(Marker i in input)
            {
                Insert(i, hierarchyDefinitions);
            }
        }
    }
}
