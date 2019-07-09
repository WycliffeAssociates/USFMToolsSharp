using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models
{
    public class HTMLConfig
    {
        public List<string> divClasses;
        public bool separateChapters;
        public bool separateVerses;
        public bool addBookHeaders;


        public HTMLConfig()
        {
            this.divClasses = new List<string>();
            separateChapters = false;
            separateVerses = false;
            addBookHeaders = true;
        }
        public HTMLConfig(List<string> divClasses, bool separateChapters=false, bool separateVerses=false,bool addBookHeaders = true):this()
        {

            this.divClasses = divClasses;
            this.separateChapters = separateChapters;
            this.separateVerses = separateVerses;
            this.addBookHeaders = addBookHeaders;
    }
        
        
    }
}
