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
        public List<string> HeaderLinks;
        public HTMLConfig ConfigurationHTML;
        

        public string FrontMatterHTML { get; set; }
        public string InsertedFooter { get; set;}
        public string InsertedHead { get; set; }

        public HtmlRenderer()
        {
            UnrenderableTags = new List<string>();
            FootnoteTextTags = new List<string>();
            HeaderLinks = new List<string>();

            ConfigurationHTML = new HTMLConfig();
        }
        public HtmlRenderer(HTMLConfig config)
        {

            ConfigurationHTML = config;

            HeaderLinks = new List<string>();
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

            
            

            foreach (string class_name in ConfigurationHTML.divClasses)
            {
                output.AppendLine($"</div>");
            }
            output.AppendLine(InsertedFooter);

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

                    // New Line after each Verse
                    if (ConfigurationHTML.separateVerses)
                    {
                        output.AppendLine("<br/>");
                    }
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
                    output.AppendLine($"<div class=\"Section-{mTMarker.Title}\">");
                    output.AppendLine($"<div class=\"majortitle-{mTMarker.Weight}\">");
                    output.AppendLine(mTMarker.Title);
                    output.AppendLine("</div>");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    
                    if (ConfigurationHTML.addBookHeaders)
                    {
                        output.AppendLine(addBookTitleHeader(mTMarker.Title));
                        output.AppendLine(InsertedFooter);
                        output.AppendLine("<br class=\"bookbreak\"></br>");
                        output.AppendLine("</div>");

                    }
                    if (!ConfigurationHTML.separateChapters && mTMarker.Weight==1)   // No double page breaks before books
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
                case FPMarker fPMarker:
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    break;
                case FTMarker fTMarker:
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    break;
                case FRMarker fRMarker:
                    output.Append($"<b> {fRMarker.VerseReference} </b>");
                    break;
                case FKMarker fKMarker:
                    output.Append($" {fKMarker.FootNoteKeyword.ToUpper()}: ");
                    break;
                case FQAMarker _:
                    output.Append("<span class=\"footnote-alternate-translation\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</span>");
                    break;
                case BMarker bMarker:
                    output.Append("<br/><br/>");
                    break;
                case SMarker sMarker:
                    output.AppendLine($"<div class=\"sectionhead-{sMarker.Weight}\">");
                    output.AppendLine(sMarker.Text);
                    output.AppendLine("</div>");
                    break;
                case BKMarker bkMarker:
                    output.AppendLine($"<span class=\"quoted-book\">");
                    output.AppendLine(bkMarker.BookTitle);
                    output.AppendLine("</span>");
                    break;
                case LIMarker liMarker:
                    output.AppendLine($"<div class=\"list-{liMarker.Depth}\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</div>");
                    break;
                case ADDMarker addMarker:
                    output.AppendLine($"<span class=\"additions\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</span>");
                    break;
                case TLMarker tlMarker:
                    output.AppendLine($"<span class=\"transliterated\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</span>");
                    break;
                case SCMarker scMarker:
                    output.AppendLine($"<span class=\"small-caps\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.AppendLine("</span>");
                    break;
                case WMarker wMarker:
                    output.AppendLine($"<span class=\"word-entry\">{wMarker.Term}</span>");
                    break;
                case WEndMarker _:
                case FQMarker fqMarker:
                    output.Append("<span class=\"footnote-alternate-translation\">");
                    foreach (Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</span>");
                    break;
                case FQEndMarker _:
                case TLEndMarker _:
                case SCEndMarker _:
                case ADDEndMarker _:
                case BKEndMarker _:
                case FQAEndMarker _:
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
                footnoteHTML.AppendLine("<div class=\"footnote-header\">Footnotes</div>");
                foreach (string footnote in FootnoteTextTags)
                {
                    footnoteHTML.AppendLine("<div class=\"footnotes\">");
                    footnoteHTML.Append(footnote);
                    footnoteHTML.AppendLine("</div>");
                }
                FootnoteTextTags.Clear();
            }
            return footnoteHTML.ToString();
        }
        private string addBookTitleHeader(string title)
        {
            string headerLink = $@"
            @page Section-{title}
	            {"{"}size:8.5in 11.0in;
	            margin:1.0in 1.0in 1.0in 1.0in;
	            mso-header-margin:.5in;
	            mso-footer-margin:.5in;
	            mso-header: h-{title};
                mso-footer: f1;
                mso-first-header: h-{title};
                mso-first-footer: f1;
                mso-paper-source:0;
                { "}"}
            div.Section-{title}
	            {"{"}
                page:Section-{title};
                {"}"}
            ";
            HeaderLinks.Add(headerLink);
            string headerHTML = $@"
            <div class=Section-{title}>
            <table id='hrdftrtbl' border='0' cellspacing='0' cellpadding='0'>
            <div class='Section-{title}'>
            <table id='hrdftrtbl' border='0' cellspacing='0' cellpadding='0'>
            <tr><td>
            <div style='mso-element:header' id='h-{title}'>
            <p class='MsoHeader'></p>
            <span class='book-title-header'>{title}</span>
            </div>
            </td></tr>
            </table>
            </div>
            </table>
            </div>";
            return headerHTML;
        }

    }
}
