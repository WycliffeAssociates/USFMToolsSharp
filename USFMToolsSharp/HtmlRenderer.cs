using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public class HtmlRenderer
    {
        public List<string> UnrenderableTags;

        public HtmlRenderer()
        {
            UnrenderableTags = new List<string>();
        }

        public string Render(USFMDocument input)
        {
            UnrenderableTags = new List<string>();
            string encoding = GetEncoding(input);
            StringBuilder output = new StringBuilder();
            output.Append("<html>");
            output.Append("<head>");
            if (!string.IsNullOrEmpty(encoding))
            {
                output.AppendLine($"<meta charset=\"{encoding}\">");
            }
            output.Append("<link rel=\"stylesheet\" href=\"style.css\">");
            output.Append("</head>");
            output.Append("<body>");

            foreach(Marker marker in input.Contents)
            {
                output.Append(RenderMarker(marker));
            }

            output.Append("</body>");
            output.Append("</html>");
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
                    output.Append("<p>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</p>");
                    break;
                case CMarker cMarker:
                    output.Append($"<div class=\"chapter\">{cMarker.Number}</div>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    break;
                case VMarker vMarker:
                    output.Append($"<span class=\"verse\">");
                    output.Append($"<span class=\"versemarker\">{vMarker.Number}</span>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append($"</span>");
                    break;
                case QMarker qMarker:
                    output.Append("<span class=\"poetry\">");
                    output.Append(qMarker.Text);
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</span>");
                    break;
                case MMarker mMarker:
                    output.Append("<div class=\"resetmargin\">");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</div>");
                    break;
                case TextBlock textBlock:
                    output.Append(textBlock.Text);
                    break;
                case BDMarker bdMarker:
                    output.Append("<b>");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</b>");
                    break;
                case HMarker hMarker:
                    output.Append("<div class=\"header\">");
                    output.Append(hMarker.HeaderText);
                    output.Append("</div>");
                    break;
                case MTMarker mTMarker:
                    output.Append("<div class=\"majortitle\">");
                    output.Append(mTMarker.Title);
                    output.Append("</div>");
                    break;
                case IDEMarker _:
                case IDMarker _:
                    break;
                default:
                    UnrenderableTags.Add(input.Identifier);
                    break;
            }
            return output.ToString();
        }
    }
}
