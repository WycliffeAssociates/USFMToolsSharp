using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace USFMToolsSharp.Models.Markers
{
    public class USFMDocument: Marker
    {
        public USFMDocument()
        {
        }

        public List<HierarchyNode> Hierarchies { get; set; } = new List<HierarchyNode>();
        public List<Marker> AllMarkers { get; set; } = new List<Marker>();
        
        public int NumberOfTotalMarkersAtParse { get; set; }
        
        private List<List<HierarchyNode>> _toLastChildPath = new List<List<HierarchyNode>>();
        private List<List<Func<Type, HierarchyNode, Marker, bool>?>> _canInsertFunctions = new();

        public void Insert(Marker input)
        {
            if (Hierarchies.Count == 0)
            {
                Hierarchies =
                [
                    new HierarchyNode(null), // Default
                    new HierarchyNode(null), // Structure
                    new HierarchyNode(null)  // Presentation
                ];
            }

            if (input is USFMDocument doc)
            {
                InsertMultiple(doc.AllMarkers, [DefaultHierarchies.Default.ToFrozenDictionary(), DefaultHierarchies.Structure.ToFrozenDictionary(), DefaultHierarchies.Presentation.ToFrozenDictionary() ] );
                return;
            }

            Insert(input, [DefaultHierarchies.Default.ToFrozenDictionary(), DefaultHierarchies.Structure.ToFrozenDictionary(), DefaultHierarchies.Presentation.ToFrozenDictionary() ] );
        }

        public void Insert(Marker input, List<FrozenDictionary<Type, HierarchyDefinition>> hierarchyDefinitions)
        {
            AllMarkers.Add(input);
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
                    _canInsertFunctions.Add([null]);
                    continue;
                }
                var currentCanInsertFunctions = _canInsertFunctions[i];
                var currentHierarchy = hierarchyDefinitions[i];
                
                var inserted = false;
                var currentPath = _toLastChildPath[i];
                
                // Check all ancestor CanInsert functions from root down
                // If any CanInsert function fails, we need to trim the path back and try to find
                // a different insertion point (the path will be further trimmed in the loop below)
                var needsToFindNewParent = false;
                for (var ii = 0; ii < currentCanInsertFunctions.Count; ii++)
                {
                    var func = currentCanInsertFunctions[ii];
                    if (func == null)
                    {
                        continue;
                    }
                    
                    var currentNode = currentPath[ii];
                    var currentNodeType = currentNode.MarkerType;

                    if (!func(currentNodeType, currentNode, input))
                    {
                        // CanInsert function failed at index ii
                        // Trim back to include this node (we'll check it in the main loop below)
                        while (currentPath.Count > ii + 1)
                        {
                            TrimPathEnd(currentPath, currentCanInsertFunctions);
                        }
                        needsToFindNewParent = true;
                        break;
                    }
                }

                // Walk back up the path to find a valid parent for this marker
                while (currentPath.Count > 0)
                {
                    var currentNode = currentPath[^1];
                    var currentNodeType = currentNode.MarkerType;
                    var currentNodeDefinition = currentHierarchy.GetValueOrDefault(currentNodeType);
                    
                    // Skip nodes without hierarchy definitions
                    if (currentNodeDefinition == null)
                    {
                        TrimPathEnd(currentPath, currentCanInsertFunctions);
                        continue;
                    }

                    // If we need to find a new parent (because CanInsert failed), skip the current node
                    if (needsToFindNewParent)
                    {
                        TrimPathEnd(currentPath, currentCanInsertFunctions);
                        continue;
                    }
                    
                    // Check if this parent allows this child type
                    if (!currentNodeDefinition.AllowedChildren.Contains(markerType))
                    {
                        TrimPathEnd(currentPath, currentCanInsertFunctions);
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
            _canInsertFunctions[i].Add(definition?.CanInsert);
            target.Contents.Add(tmp);
            return tmp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TrimPathEnd(List<HierarchyNode> path, List<Func<Type, HierarchyNode, Marker, bool>?> functions)
        {
            path.RemoveAt(path.Count - 1);
            functions.RemoveAt(functions.Count - 1);
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
        [Obsolete("Use Hierarchies[0].Contents instead")]
        public List<HierarchyNode> Contents => Hierarchies.Count == 0 ? [] : Hierarchies[0].Contents;

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
