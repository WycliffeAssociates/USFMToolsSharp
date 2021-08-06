using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    public abstract class Marker
    {
        public Marker()
        {
            Contents = new List<Marker>();
        }
        public List<Marker> Contents;
        public abstract string Identifier { get; }
        public int Position { get; set; }
        public virtual List<Type> AllowedContents {
            get {
                return new List<Type>();
            }
        }

        /// <summary>
        /// Pre-process the text contents before creating text elements inside of it
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string PreProcess(string input)
        {
            return input;
        }

        public virtual bool TryInsert(Marker input)
        {
            if(Contents.Count > 0 && Contents[Contents.Count - 1].TryInsert(input))
            {
                return true;
            }
            if (AllowedContents.Contains(input.GetType()))
            {
                Contents.Add(input);
                return true;
            }
            return false;
        }
        public List<Type> GetTypesPathToLastMarker() 
        {
            List<Type> types = new List<Type>();
            types.Add(GetType());
            if (Contents.Count > 0 )
            {
                types.AddRange(Contents[Contents.Count - 1].GetTypesPathToLastMarker());
            }
            return types;
        }

        public List<Marker> GetHierarchyToMarker(Marker target)
        {
            var parents = new Stack<Marker>();
            int childMarkerContentsCount;

            bool found = false;
            var stack = new Stack<(Marker marker, bool isLastInParent)>();
            stack.Push((this, true));
            while (stack.Count > 0)
            {
                var (marker, isLastInParent) = stack.Pop();
                if (marker == target)
                {
                    found = true;
                    break;
                }

                if (marker.Contents.Count != 0)
                {
                    // We're descending
                    parents.Push(marker);

                    childMarkerContentsCount = marker.Contents.Count;
                    for (int i = 0; i < childMarkerContentsCount; i++)
                    {
                        stack.Push((marker.Contents[i], i == childMarkerContentsCount - 1));
                    }
                }
                else if (stack.Count == 0 || isLastInParent)
                {
                    // We're ascending
                    parents.Pop();
                }
            }
            var output = new List<Marker>();

            if (!found)
            {
                return output;
            }

            output.Add(target);
            output.AddRange(parents);
            output.Reverse();
            return output;
        }

        /// <summary>
        /// Get the paths to multiple markers
        /// </summary>
        /// <param name="targets">A list of markers to find</param>
        /// <returns>A dictionary of markers and paths</returns>
        /// <remarks>In the case that the marker doesn't exist in the tree the dictionary will contain an empty list for that marker</remarks>
        public Dictionary<Marker, List<Marker>> GetHierachyToMultipleMarkers(List<Marker> targets)
        {
            Dictionary<Marker, List<Marker>> output = new Dictionary<Marker, List<Marker>>(targets.Count);
            var parents = new Stack<Marker>();
            int childMarkerContentsCount;

            var stack = new Stack<(Marker marker, bool isLastInParent)>();
            stack.Push((this, true));
            while (stack.Count > 0)
            {
                var (marker, isLastInParent) = stack.Pop();
                if (targets.Contains(marker))
                {
                    var tmp = new List<Marker>(parents.Count + 1)
                    {
                        marker
                    };
                    tmp.AddRange(parents);
                    tmp.Reverse();
                    output.Add(marker, tmp);
                    if (output.Count == targets.Count)
                    {
                        break;
                    }
                }

                if (marker.Contents.Count != 0)
                {
                    // We're descending
                    parents.Push(marker);

                    childMarkerContentsCount = marker.Contents.Count;
                    for (int i = 0; i < childMarkerContentsCount; i++)
                    {
                        stack.Push((marker.Contents[i], i == 0));
                    }
                }
                else if (stack.Count == 0 || isLastInParent)
                {
                    // We're ascending
                    parents.Pop();
                }
            }

            foreach (var i in targets)
            {
                if (!output.ContainsKey(i))
                {
                    output.Add(i, new List<Marker>());
                }
            }

            return output;
        }
        /// <summary>
        /// A recursive search for children of a certain type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetChildMarkers<T>(List<Type> ignoredParents = null) where T : Marker
        {
            List<T> output = new List<T>();
            var stack = new Stack<Marker>(Contents);

            if (ignoredParents != null && ignoredParents.Contains(this.GetType()))
            {
                return output;
            }

            while (stack.Count > 0)
            {
                var marker = stack.Pop();
                if (marker is T)
                {
                    output.Add((T)marker);
                }
                foreach (var child in marker.Contents)
                {
                    if (ignoredParents == null || !ignoredParents.Contains(child.GetType()))
                    {
                        stack.Push(child);
                    }
                }
            }

            output.Reverse();
            return output;
        }

        public Marker GetLastDescendent()
        {
            if (Contents.Count == 0)
            {
                return this;
            }

            return Contents[Contents.Count - 1].GetLastDescendent();
        }
    }
}
