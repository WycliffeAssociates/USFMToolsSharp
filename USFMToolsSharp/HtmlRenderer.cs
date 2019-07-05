using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models.Markers;
using USFMToolsSharp.Models;

namespace USFMToolsSharp
{
    public class HtmlRenderer
    {
        public List<string> UnrenderableTags;
        public List<string> FootnoteTextTags;
        public HTMLConfig ConfigurationHTML;
        

        public string FrontMatterHTML { get; set; }
        public string InsertedFooter { get; set;}
        public string InsertedHead { get; set; }

        public HtmlRenderer()
        {
            UnrenderableTags = new List<string>();
            FootnoteTextTags = new List<string>();

            ConfigurationHTML = new HTMLConfig();
        }
        public HtmlRenderer(HTMLConfig config)
        {

            ConfigurationHTML = config;

            UnrenderableTags = new List<string>();
            FootnoteTextTags = new List<string>();

        }

        public string Render(USFMDocument input)
        {
            UnrenderableTags = new List<string>();
            var encoding = GetEncoding(input);
            StringBuilder output = new StringBuilder();
            output.AppendLine("<html>");

            if (InsertedHead == null)
            {
                output.AppendLine("<head>");
                if (!string.IsNullOrEmpty(encoding))
                {
                    output.Append($"<meta charset=\"{encoding}\">");
                }
                output.AppendLine("<link rel=\"stylesheet\" href=\"style.css\">");

                output.AppendLine("</head>");
            }
            else
            {
                output.AppendLine(InsertedHead);
            }

            output.AppendLine("<body>");

            output.AppendLine(FrontMatterHTML);

            // HTML tags can only have one class, when render to docx

            foreach(string class_name in ConfigurationHTML.divClasses)
            {
                output.AppendLine($"<div class=\"{class_name}\">");
            }

            

            foreach (Marker marker in input.Contents)
            {
                output.Append(RenderMarker(marker));
            }

            
            output.AppendLine(RenderFootnotes());

            foreach (string class_name in ConfigurationHTML.divClasses)
            {
                output.AppendLine($"</div>");
            }


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
                    if (ConfigurationHTML.separateChapters)
                    {
                        output.AppendLine("<br class=\"pagebreak\"></br>");
                    }

                    break;
                case VMarker vMarker:
                    output.AppendLine($"<span class=\"verse\">");
                    output.AppendLine($"<span class=\"versemarker\">{vMarker.VerseCharacter}</span>");
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

                    if (!ConfigurationHTML.separateChapters)   // No double page breaks before books
                    {
                        output.AppendLine("<br class=\"pagebreak\"></br>");
                    }
                    break;
                case FMarker fMarker:
                    StringBuilder footnote = new StringBuilder();

                    string footnoteId;
                    switch (fMarker.FootNoteCaller)
                    {
                        case "-":
                            footnoteId = "";
                            break;
                        case "+":
                            footnoteId = $"{FootnoteTextTags.Count+1}";
                            break;
                        default:
                            footnoteId = fMarker.FootNoteCaller;
                            break;

                    }

                    string footnoteCallerHTML = $"<span class=\"footnotecaller\">{footnoteId}</span>";
                    output.AppendLine(footnoteCallerHTML);
                    footnote.Append(footnoteCallerHTML);
                    foreach (Marker marker in input.Contents)
                    {
                        footnote.Append(RenderMarker(marker));
                    }
                    FootnoteTextTags.Add(footnote.ToString());
                    break;
                case FTMarker fTMarker:
                    
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    
                    break;
                case FQAMarker _:
                    output.Append("<span class=\"footnote-alternate-translation\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    break;
                case FQAEndMarker fQAEndMarker:
                    output.Append("</span>");
                    break;
                case BMarker bMarker:
                    output.Append("<br/><br/>");
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
