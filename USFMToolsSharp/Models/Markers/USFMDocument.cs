using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
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
        
        private List<Stack<HierachyNode>> _toLastChildStack = new List<Stack<HierachyNode>>();
        

        public void Insert(Marker input, List<ReadOnlyDictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            var markerType = input.GetType();
            for (var i = 0; i < hierarchyDefinitions.Count; i++)
            {
                if (Hierarchies[i].Contents.Count == 0)
                {
                    var firstNode = new HierachyNode(input);
                    Hierarchies[i].Contents.Add(firstNode);
                    _toLastChildStack.Add(new Stack<HierachyNode>([firstNode]));
                    return;
                }
                
                var inserted = false;
                var currentStack = _toLastChildStack[i];
                while (currentStack.Count > 0)
                {
                    var currentNode = currentStack.Peek();
                    var currentNodeType = currentNode.MarkerType;
                    var currentNodeDefinition = hierarchyDefinitions[i].GetValueOrDefault(currentNodeType);
                    if (currentNodeDefinition == null || currentNodeDefinition.CanInsert != null && !currentNodeDefinition.CanInsert(currentNodeType, currentNode, input) || !currentNodeDefinition.AllowedChildren.Contains(markerType))
                    {
                        currentStack.Pop();
                        continue;
                    }

                    InsertNode(input, i, currentNode);
                    // Going down
                    inserted = true;
                    break;
                }

                if (!inserted)
                {
                    InsertNode(input, i, Hierarchies[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InsertNode(Marker input, int i, HierachyNode target)
        {
            var tmp = new HierachyNode(input);
            _toLastChildStack[i].Push(tmp);
            target.Contents.Add(tmp);
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
        
        public void InsertMultiple(IEnumerable<Marker> input, List<ReadOnlyDictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            foreach(Marker i in input)
            {
                Insert(i, hierarchyDefinitions);
            }
        }
    }
}
