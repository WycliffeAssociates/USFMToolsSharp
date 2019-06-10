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
        public HtmlRenderer()
        {
            UnrenderableTags = new List<string>();
            FootnoteTextTags = new List<string>();
        }
        public HtmlRenderer(bool isSingleSpaced, bool hasOneColumn, bool isL2RDirection, bool isTextJustified, bool separateChapters)
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
            output.AppendLine("</head>");
            
            // HTML tags can only have one class, when render to docx
            output.AppendLine($"<body class=\"{(isSingleSpaced ? "" : "double-space")}\">");
            output.AppendLine($"<div class=\"{ (hasOneColumn ? "" : "multi-column")}\">");
            output.AppendLine($"<div class=\"{ (isTextJustified ? "justified" : "")}\"> ");
            output.AppendLine($"<div class=\"{ (isL2RDirection ? "" : "rtl-direct")}\"> ");

            foreach (Marker marker in input.Contents)
            {
                output.Append(RenderMarker(marker));
            }

            output.AppendLine(RenderFootnotes());

            output.AppendLine("</div>");
            output.AppendLine("</div>");
            output.AppendLine("</div>");
            output.AppendLine("</div>");
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

                    // Page breaks after each chapter
                    output.AppendLine($"{(separateChapters?"<br class=\"pagebreak\"></br>" : "")}");
                    break;
                case VMarker vMarker:
                    output.AppendLine($"<span class=\"verse\">");
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
                    break;
                case FMarker fMarker:
                    foreach (Marker marker in input.Contents)
                    {
                        RenderMarker(marker);
                    }
                    break;
                case FTMarker fTMarker:
                    string footnote="";
                    foreach(Marker marker in input.Contents)
                    {
                        switch (marker)
                        {
                            case TextBlock textBlock:
                                footnote += textBlock.Text+" ";
                                break;
                            case FQAMarker fQAMarker:
                                footnote += "<span class=\"footnote-quote\">";
                                break;
                            default:
                                break;
                        }

                    }
                    FootnoteTextTags.Add(footnote);
                    break;
                case FQAEndMarker fQAEndMarker:
                    FootnoteTextTags[FootnoteTextTags.Count-1] += "</span>" + " ";
                    break;

                //case FootnoteTextBlock footnoteTextBlock:
                //    FootnoteTextTags.Add(footnoteTextBlock.Text);
                //    break;
                case IDEMarker _:
                case IDMarker _:
                    break;
                default:
                    UnrenderableTags.Add(input.Identifier);
                    break;
            }
            return output.ToString();
        }
        private string RenderFootnotes()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("<br class=\"pagebreak\"></br>");
            output.AppendLine("<div class=\"header\">Footnotes</div>");
            foreach (string footnote in FootnoteTextTags)
            {
                output.AppendLine("<div>");
                output.Append(footnote);
                output.AppendLine("</div>");
            }
            return output.ToString();
        }
    }
}
