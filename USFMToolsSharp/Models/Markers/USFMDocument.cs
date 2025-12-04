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

        public List<HierachyNode> Hierarchies { get; set; } = new List<HierachyNode>();
        
        public int NumberOfTotalMarkersAtParse { get; set; }
        
        private List<HierachyNode> _lastChildNode = [];
        private List<Stack<HierachyNode>> _toLastChildStack = new List<Stack<HierachyNode>>();
        

        public void Insert(Marker input, List<Dictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            var markerType = input.GetType();
            for (var i = 0; i < hierarchyDefinitions.Count; i++)
            {
                if (Hierarchies[i].Contents.Count == 0)
                {
                    var firstNode = new HierachyNode(input);
                    Hierarchies[i].Contents.Add(firstNode);
                    _toLastChildStack.Add(new Stack<HierachyNode>());
                    _lastChildNode.Add(firstNode);
                    return;
                }
                var stack = new Stack<HierachyNode>();
                var toLastChildStack = new Stack<HierachyNode>();
                var lastChildNode = Hierarchies[i].Contents[^1];
                toLastChildStack.Push(Hierarchies[i].Contents[^1]);
                stack.Push(Hierarchies[i].Contents[^1]);
                while (lastChildNode.Contents.Count > 0)
                {
                    lastChildNode = lastChildNode.Contents[^1];
                    stack.Push(lastChildNode);
                }
                var inserted = false;
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    var currentNodeType = currentNode.Marker.GetType();
                    var currentNodeDefinition = hierarchyDefinitions[i].GetValueOrDefault(currentNodeType);
                    if (currentNodeDefinition == null)
                    {
                        continue;
                    }
                    if (currentNodeDefinition.CanInsert != null && !currentNodeDefinition.CanInsert(currentNodeType, currentNode, input))
                    {
                        continue;
                    }
                    if (!currentNodeDefinition.AllowedChildren.Contains(markerType))
                    {
                        continue;
                    }
                    currentNode.Contents.Add(new HierachyNode(input));
                    inserted = true;
                    break;
                }

                if (!inserted)
                {
                    Hierarchies[i].Contents.Add(new HierachyNode(input));
                }
            }
        }
        public List<Type> GetTypesPathToLastMarker(int hierarchyIndex = 0)
        {
            List<Type> types = new List<Type>();
            if (Hierarchies.Count == 0)
            {
                return types;
            }
            var currentNode = Hierarchies[hierarchyIndex];
            types.Add(currentNode.Marker != null ? currentNode.Marker.GetType() : typeof(USFMDocument));
            while (currentNode.Contents.Count > 0)
            {
                currentNode = currentNode.Contents[^1];
                if (currentNode.Marker == null)
                {
                    continue;
                }
                types.Add(currentNode.Marker.GetType());
            }
            return types;
        }
        
        // Backwards compatibility
        [Obsolete("Use Hierachies[0].Contents instead")]
        public List<HierachyNode> Contents => Hierarchies[0].Contents;
        
        public void InsertMultiple(IEnumerable<Marker> input, List<Dictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            foreach(Marker i in input)
            {
                Insert(i, hierarchyDefinitions);
            }
        }
    }
}
