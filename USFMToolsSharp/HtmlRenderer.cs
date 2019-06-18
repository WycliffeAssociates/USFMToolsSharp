using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public class HtmlRenderer
    {
        public List<string> UnrenderableTags;
        public List<string> FootnoteTextTags;

        private bool isSingleSpaced = true;
        private bool hasOneColumn = true;
        private bool isL2RDirection = true;
        private bool isTextJustified = false;
        private bool separateChapters = false;

        public string FrontMatterHTML { get; set; }
        public string InsertedFooter { get; set;}

        public HtmlRenderer()
        {
            UnrenderableTags = new List<string>();
            FootnoteTextTags = new List<string>();
        }
        public HtmlRenderer(bool isSingleSpaced=true, bool hasOneColumn=true, bool isL2RDirection=true, bool isTextJustified=false, bool separateChapters=false)
        {
            this.isSingleSpaced = isSingleSpaced;
            this.hasOneColumn = hasOneColumn;
            this.isL2RDirection = isL2RDirection;
            this.isTextJustified = isTextJustified;
            this.separateChapters = separateChapters;

            UnrenderableTags = new List<string>();
            FootnoteTextTags = new List<string>();

        }

        public string Render(USFMDocument input)
        {
            UnrenderableTags = new List<string>();
            string encoding = GetEncoding(input);
            StringBuilder output = new StringBuilder();
            output.AppendLine("<html>");
            output.AppendLine("<head>");
            if (!string.IsNullOrEmpty(encoding))
            {
                output.Append($"<meta charset=\"{encoding}\">");
            }
            output.AppendLine("<link rel=\"stylesheet\" href=\"style.css\">");

            if (FrontMatterHTML.Contains("</head>"))
            {
                output.AppendLine(FrontMatterHTML);
            }
            else {
                output.AppendLine("</head>");
            }
            
            // HTML tags can only have one class, when render to docx
            output.AppendLine($"<body{(isSingleSpaced ? "" : " class=\"double-space\">")}");
            output.AppendLine($"{ (hasOneColumn ? "" : "< div class=\"multi-column\">")}");

            output.AppendLine($"{ (isTextJustified ? "<div class=\"justified\">" : "")}");
            output.AppendLine($"{ (isL2RDirection ? "" : "<div class=\"rtl-direct\">")}");

            

            foreach (Marker marker in input.Contents)
            {
                output.Append(RenderMarker(marker));
            }

            output.AppendLine(InsertedFooter);
            output.AppendLine(RenderFootnotes());

            output.AppendLine($"{ (hasOneColumn ? "" :"</div>")}");
            output.AppendLine($"{ (isTextJustified ? "":"</div>")}");
            output.AppendLine($"{ (isL2RDirection ? "" : "</div>")}");
            output.AppendLine("</body>");
            output.AppendLine("</html>");
            return output.ToString();
        }

        private string GetEncoding(USFMDocument input)
        {
            var encodingSearch = input.GetChildMarkers<IDEMarker>();
            if (encodingSearch.Count > 0)
            {
                return encodingSearch[0].Encoding;
            }
            return null;
        }

        private string RenderMarker(Marker input)
        {
            StringBuilder output = new StringBuilder();
            switch (input)
            {
                case PMarker _:
                    output.AppendLine("<p>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</p>");
                    break;
                case CMarker cMarker:
                    output.AppendLine("<div class=\"chapter\">");
                    output.AppendLine($"<span class=\"chaptermarker\">{cMarker.Number}</span>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</div>");
                    output.AppendLine(RenderFootnotes());
                    // Page breaks after each chapter
                    output.AppendLine($"{(separateChapters ? "<br class=\"pagebreak\"></br>" : "")}");

                    break;
                case VMarker vMarker:
                    output.AppendLine($"<span class=\"verse\">");

                    //Interpret VPMarker

                    switch (input.Contents[0])
                    {
                        case VPMarker vPMarker:
                            vMarker.VerseCharacter=vPMarker.VerseCharacter;
                            break;
                        default:
                            break;
                    }

                    output.AppendLine($"<span class=\"versemarker\">{vMarker.VerseNumber}</span>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine($"</span>");
                    break;
                case QMarker qMarker:
                    output.AppendLine($"<div class=\"poetry-{qMarker.Depth}\">");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</div>");

                    break;
                case MMarker mMarker:

                    output.AppendLine("<div class=\"resetmargin\">");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</div>");
                    break;
                case TextBlock textBlock:
                    output.AppendLine(textBlock.Text);
                    break;
                case BDMarker bdMarker:
                    output.AppendLine("<b>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</b>");
                    break;
                case HMarker hMarker:
                    

                    output.AppendLine("<div class=\"header\">");
                    output.Append(hMarker.HeaderText);
                    output.AppendLine("</div>");
                    
                    break;
                case MTMarker mTMarker:
                    output.AppendLine("<div class=\"majortitle\">");
                    output.AppendLine(mTMarker.Title);
                    output.AppendLine("</div>");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine($"{(separateChapters ? "" : "<br class=\"pagebreak\"></br>")}");
                    break;
                case FMarker fMarker:
                    output.AppendLine("<div class=\"footnote\">");
                    foreach (Marker marker in input.Contents)
                    {
                        RenderMarker(marker);
                    }
                    output.AppendLine("</div>");
                    break;
                case FTMarker fTMarker:
                    StringBuilder footnote=new StringBuilder();
                    foreach(Marker marker in input.Contents)
                    {
                        footnote.Append(RenderMarker(marker));
                    }
                    FootnoteTextTags.Add(footnote.ToString());
                    break;
                case FQAMarker _:
                    output.Append("<span class=\"footnote-alternate-translation\">");
                    break;
                case FQAEndMarker fQAEndMarker:
                    if(FootnoteTextTags.Count > 0)
                        FootnoteTextTags[FootnoteTextTags.Count-1] += "</span>" + " ";
                    break;
                case FEndMarker _:
                case IDEMarker _:
                case IDMarker _:
                case VPMarker _:
                case VPEndMarker _:
                    break;
                default:
                    UnrenderableTags.Add(input.Identifier);
                    break;
            }
            return output.ToString();
        }
        private string RenderFootnotes()
        {
            StringBuilder footnoteHTML = new StringBuilder();
            if (FootnoteTextTags.Count > 0)
            {
                footnoteHTML.AppendLine("<div class=\"header\">Footnotes</div>");
                foreach (string footnote in FootnoteTextTags)
                {
                    footnoteHTML.AppendLine("<div>");
                    footnoteHTML.Append(footnote);
                    footnoteHTML.AppendLine("</div>");
                }
                FootnoteTextTags.Clear();
            }
            return footnoteHTML.ToString();
        }
        
    }
}
