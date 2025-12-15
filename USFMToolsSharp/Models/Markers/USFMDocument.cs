using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public class USFMDocument: Marker
    {
        public USFMDocument()
        {
        }

        public List<HierarchyNode> Hierarchies { get; set; } = new List<HierarchyNode>();
        
        public int NumberOfTotalMarkersAtParse { get; set; }
        
        private List<List<HierarchyNode>> _toLastChildPath = new List<List<HierarchyNode>>();
        private List<Func<Type, HierarchyNode, Marker, bool>?> _canInsertFunctions = new();
        

        public void Insert(Marker input, List<FrozenDictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            var markerType = input.GetType();
            for (var i = 0; i < hierarchyDefinitions.Count; i++)
            {
                var isDefaultHierarchy = i == 0;
                if (Hierarchies[i].Contents.Count == 0)
                {
                    var firstNode = new HierarchyNode(input);
                    if (isDefaultHierarchy)
                    {
                        input.DefaultHierarchyNode = firstNode;
                    }
                    Hierarchies[i].Contents.Add(firstNode);
                    _toLastChildPath.Add([firstNode]);
                    _canInsertFunctions.Add(null);
                    return;
                }
                var currentHierarchy = hierarchyDefinitions[i];
                
                var inserted = false;
                var currentPath = _toLastChildPath[i];
                
                // Check all ancestor CanInsert functions from root down
                var canInsertBasedOnFunctions = true;
                var distanceAtWhichFailed = -1;
                for (var ii = 0; ii < _canInsertFunctions.Count; ii++)
                {
                    var func = _canInsertFunctions[ii];
                    if (func == null)
                    {
                        continue;
                    }
                    
                    var currentNode = currentPath[ii];
                    var currentNodeType = currentNode.MarkerType;

                    if (!func(currentNodeType, currentNode, input))
                    {
                        canInsertBasedOnFunctions = false;
                        distanceAtWhichFailed = ii;
                        break;
                    }
                }
                
                // If a CanInsert function failed at index ii, trim back to the parent (index ii-1)
                // This means removing everything from index ii onwards
                if (distanceAtWhichFailed >= 0)
                {
                    var targetDepth = distanceAtWhichFailed;
                    while (currentPath.Count > targetDepth)
                    {
                        currentPath.RemoveAt(currentPath.Count - 1);
                        _canInsertFunctions.RemoveAt(_canInsertFunctions.Count - 1);
                    }
                    canInsertBasedOnFunctions = false;
                }

                // Now walk back up the path to find a valid parent for this marker
                while (currentPath.Count > 0)
                {
                    var currentNode = currentPath[^1];
                    var currentNodeType = currentNode.MarkerType;
                    var currentNodeDefinition = currentHierarchy.GetValueOrDefault(currentNodeType);
                    
                    if (currentNodeDefinition == null)
                    {
                        currentPath.RemoveAt(currentPath.Count - 1);
                        _canInsertFunctions.RemoveAt(_canInsertFunctions.Count - 1);
                        continue;
                    }

                    if (!canInsertBasedOnFunctions)
                    {
                        currentPath.RemoveAt(currentPath.Count - 1);
                        _canInsertFunctions.RemoveAt(_canInsertFunctions.Count - 1);
                        continue;
                    }
                    
                    if (!currentNodeDefinition.AllowedChildren.Contains(markerType))
                    {
                        currentPath.RemoveAt(currentPath.Count - 1);
                        _canInsertFunctions.RemoveAt(_canInsertFunctions.Count - 1);
                        continue;
                    }

                    var node = InsertNode(input, i, currentNode, currentNodeDefinition);
                    if (isDefaultHierarchy)
                    {
                        input.DefaultHierarchyNode = node;
                    }
                    inserted = true;
                    break;
                }

                if (!inserted)
                {
                    var node = InsertNode(input, i, Hierarchies[i], null);
                    if (isDefaultHierarchy)
                    {
                        input.DefaultHierarchyNode = node;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private HierarchyNode InsertNode(Marker input, int i, HierarchyNode target, HierarchyDefinition? definition)
        {
            var tmp = new HierarchyNode(input);
            _toLastChildPath[i].Add(tmp);
            _canInsertFunctions.Add(definition?.CanInsert);
            target.Contents.Add(tmp);
            return tmp;
        }

        public List<Type> GetTypesPathToLastMarker(int hierarchyIndex = 0)
        {
            List<Type> types = new List<Type>();
            if (Hierarchies.Count == 0)
            {
                return types;
            }
            var currentNode = Hierarchies[hierarchyIndex];
            types.Add(currentNode.Marker != null ? currentNode.MarkerType : typeof(USFMDocument));
            while (currentNode.Contents.Count > 0)
            {
                currentNode = currentNode.Contents[^1];
                if (currentNode.Marker == null)
                {
                    continue;
                }
                types.Add(currentNode.MarkerType);
            }
            return types;
        }
        
        // Backwards compatibility
        [Obsolete("Use Hierachies[0].Contents instead")]
        public List<HierarchyNode> Contents => Hierarchies[0].Contents;
        
        public void InsertMultiple(IEnumerable<Marker> input, List<FrozenDictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            foreach(Marker i in input)
            {
                Insert(i, hierarchyDefinitions);
            }
        }

        public override string Identifier { get; }
    }
}
