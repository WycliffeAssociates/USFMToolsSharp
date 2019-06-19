using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Footnote marker
    /// </summary>
    public class FMarker : Marker
    {
        public override string Identifier => "f";
        public string FootNoteCaller;

        // Placeholder 
        private string footnoteId = "";
        public string FootNoteID{
            get {
                return footnoteId;
            }
            set {
                switch (FootNoteCaller)
                {
                    case "-":
                        footnoteId = "";
                        break;
                    case "+":
                        footnoteId = value;
                        break;
                    default:
                        footnoteId = FootNoteCaller;
                        break;

                }
            }
        }
        public override string PreProcess(string input)
        {
            FootNoteCaller = input;
            return string.Empty;
        }
        public override List<Type> AllowedContents => new List<Type>()
        {
            typeof(FTMarker),
            typeof(FQAMarker),
            typeof(FQAEndMarker),
            typeof(TextBlock),
        };
    }
}
