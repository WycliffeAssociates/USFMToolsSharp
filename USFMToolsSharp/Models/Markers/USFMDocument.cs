using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private LinkedList<Func<Type, HierachyNode, Marker, bool>?> _canInsertFunctions = new();
        

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
                    _canInsertFunctions.AddLast(new LinkedListNode<Func<Type, HierachyNode, Marker, bool>?>(null));
                    return;
                }
                
                var inserted = false;
                var currentStack = _toLastChildStack[i];
                while (currentStack.Count > 0)
                {
                    var currentNode = currentStack.Peek();
                    var currentNodeType = currentNode.MarkerType;
                    var currentNodeDefinition = hierarchyDefinitions[i].GetValueOrDefault(currentNodeType);
                    var canInsertBasedOnFunctions = true;
                    if (currentNodeDefinition == null)
                    {
                        currentStack.Pop();
                        continue;
                    }
                    foreach(var func in _canInsertFunctions.Where(i => i != null))
                    {
                        if (!func(currentNodeType, currentNode, input))
                        {
                            canInsertBasedOnFunctions = false;
                            break;
                        }
                    }
                    if (!canInsertBasedOnFunctions)
                    {
                        _canInsertFunctions.RemoveLast();
                        currentStack.Pop();
                        continue;
                    }
                    
                    if (!currentNodeDefinition.AllowedChildren.Contains(markerType))
                    {
                        _canInsertFunctions.RemoveLast();
                        currentStack.Pop();
                        continue;
                    }

                    InsertNode(input, i, currentNode, currentNodeDefinition);
                    // Going down
                    inserted = true;
                    break;
                }

                if (!inserted)
                {
                    InsertNode(input, i, Hierarchies[i], null);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InsertNode(Marker input, int i, HierachyNode target, HierarchyDefinition? definition)
        {
            var tmp = new HierachyNode(input);
            _toLastChildStack[i].Push(tmp);
            _canInsertFunctions.AddLast(definition?.CanInsert);
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
