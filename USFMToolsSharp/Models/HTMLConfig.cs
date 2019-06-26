﻿using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models
{
    public class HTMLConfig
    {
        public List<string> divClasses;
        public bool separateChapters;


        public HTMLConfig()
        {
            this.divClasses = new List<string>();
            separateChapters = false;
        }
        public HTMLConfig(List<string> classNames, bool separateChapters=false)
        {
            this.divClasses = classNames;
            this.separateChapters = separateChapters;
        }
        
        
    }
}
