using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public class HtmlRenderer
    {
        public string Render(USFMDocument input)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<html>");
            output.Append("<head>");
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
        public string RenderMarker(Marker input)
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
                    output.Append($"<div class=\"verse\">");
                    output.Append($"<div class=\"versemarker\">{vMarker.Number}</div>");
                    output.Append(vMarker.Text);
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append($"</div>");
                    break;
                case QMarker qMarker:
                    output.Append("<div class=\"poetry\">");
                    output.Append(qMarker.Text);
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</div>");
                    break;
                case MMarker mMarker:
                    output.Append("<div class=\"resetmargin\">");
                    foreach(Marker marker in input.Contents)
                    {
                        output.Append(RenderMarker(marker));
                    }
                    output.Append("</div>");
                    break;
                default:
                    Console.WriteLine($"HTML renderer is unable to handle {input.Identifier}");
                    break;
            }
            return output.ToString();
        }
    }
}
