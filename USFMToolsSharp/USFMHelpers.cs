using System;
using System.Collections.Generic;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public static class USFMHelpers
    {
        /// <summary>
        /// Flatten a USFM marker's children into a list of markers
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>This is a destructive operation it will modify the given input</remarks>
        /// <returns></returns>
        public static List<Marker> Flatten(Marker input)
        {
            var output = new List<Marker>();
            var stack = new Stack<Marker>();
            stack.Push(input);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                // loop through and add all the children to the stack starting backwards
                for (var i = current.Contents.Count - 1; i >= 0; i--)
                {
                    stack.Push(current.Contents[i]);
                }
                
                current.Contents.Clear();
                output.Add(current);
            }

            return output;
        }
        public static Marker Construct(List<Marker> input, Dictionary<Type,List<Type>> hierarchy = null)
        {
            hierarchy ??= Hierarchies.Original;
            var lastMarkers = new Stack<Marker>();
            foreach (var currentMarker in input)
            {
                if (lastMarkers.Count <= 0)
                {
                    lastMarkers.Push(currentMarker);
                    continue;
                }
                while (lastMarkers.Count > 0 )
                {
                    var lastMarker = lastMarkers.Peek();
                    if (hierarchy.ContainsKey(lastMarker.GetType()))
                    {
                        if (hierarchy[lastMarker.GetType()].Contains(currentMarker.GetType()))
                        {
                            lastMarker.Contents.Add(currentMarker);
                            lastMarkers.Push(currentMarker);
                            break;
                        }
                    }

                    if (lastMarker is USFMDocument)
                    {
                        lastMarker.Contents.Add(currentMarker);
                        lastMarkers.Push(currentMarker);
                        break;
                    }
            
                    lastMarkers.Pop();
                }
            }
            return input[0];
        }
    }
}