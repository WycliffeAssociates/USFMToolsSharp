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
