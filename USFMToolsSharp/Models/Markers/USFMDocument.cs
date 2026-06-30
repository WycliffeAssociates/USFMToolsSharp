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
                InsertMultiple(doc.AllMarkers, DefaultHierarchies.DefaultHierarchyList );
                return;
            }

            Insert(input, DefaultHierarchies.DefaultHierarchyList );
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

                // A node's CanInsert can refuse to hold this marker. When an ancestor
                // refuses, the marker must break out of that ancestor's entire subtree, so find
                // the shallowest ancestor that refuses it and trim the path back to just above
                // it before looking for a parent below.
                for (var ii = 0; ii < currentPath.Count; ii++)
                {
                    var ancestor = currentPath[ii];
                    var ancestorDefinition = currentHierarchy.GetValueOrDefault(ancestor.MarkerType);
                    if (ancestorDefinition?.CanInsert?.Invoke(ancestor.MarkerType, ancestor, input) == false)
                    {
                        while (currentPath.Count > ii)
                        {
                            TrimPathEnd(currentPath, currentCanInsertFunctions);
                        }
                        break;
                    }
                }

                // Walk back up the remaining path to find a parent that allows this child type.
                while (currentPath.Count > 0)
                {
                    var currentNode = currentPath[^1];
                    var currentNodeType = currentNode.MarkerType;
                    var currentNodeDefinition = currentHierarchy.GetValueOrDefault(currentNodeType);

                    // Skip nodes without hierarchy definitions or that don't allow this child type
                    if (currentNodeDefinition == null ||
                        !currentNodeDefinition.AllowedChildren.Contains(markerType))
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
