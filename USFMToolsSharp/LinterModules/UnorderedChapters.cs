using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    public class UnorderedChapters : ILinterModule
    {
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<int> chapterIndecies = new List<int>(); 
            List<LinterResult> results = new List<LinterResult>();
            foreach (Marker marker in input.Contents)
            {
                if(marker.GetType() == typeof(CMarker))
                {
                    int chapIndex = ((CMarker)marker).Number;
                    if (chapIndex != chapterIndecies[chapterIndecies.Count - 1] + 1 && chapterIndecies.Count != 0)
                    {
                        results.Add(new LinterResult {
                            Position = marker.Position,
                            Level = LinterLevel.Error,
                            Message = "Unordered Chapter Marker"
                        });
                    }
                    chapterIndecies.Add(((CMarker)marker).Number);
                }
                if(marker.GetType() == typeof(IDMarker))
                {
                    chapterIndecies.Clear();
                }
            }
            return results;
        }
    }
}
