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

        public bool TryInsert(Marker input)
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
            List<Marker> output = new List<Marker>
            {
                this
            };

            if (target == this)
            {
                return output;
            }

            List<Marker> tmp;
            foreach(Marker marker in this.Contents)
            {
                tmp = marker.GetHierarchyToMarker(target);
                if(tmp.Count != 0)
                {
                    output.AddRange(tmp);
                    return output;
                }
            }
            return new List<Marker>();
        }
        /// <summary>
        /// A recursive search for children of a certain type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetChildMarkers<T>() where T : Marker
        {
            List<T> output = new List<T>();

            foreach(Marker i in Contents)
            {
                if(i is T)
                {
                    output.Add((T)i);
                }
                output.AddRange(i.GetChildMarkers<T>());
            }

            return output;
        }
    }
}
