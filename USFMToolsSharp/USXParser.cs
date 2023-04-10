using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using USFMToolsSharp.Models.Markers;
using USFMToolsSharp.Models;

namespace USFMToolsSharp
{
    public class USXParser
    {
        private List<string> _tagsToIgnore;
        private bool _ignoreUnknownMarkers;
        public USXParser(List<string> tagsToIgnore = null, bool ignoreUnknownMarkers = false)
        {
            _tagsToIgnore = tagsToIgnore ?? new List<string>();
            _ignoreUnknownMarkers = ignoreUnknownMarkers;
        }
        public USFMDocument Parse(string content)
        {
            var markers = new List<Marker>();
            // Load xml document
            var doc = new XmlDocument();
            doc.LoadXml(content);
            // Get the root element
            var root = doc.DocumentElement;
            // Add all the children to the stack
            var nodes = new Stack<XmlNode>();
            // Add all nodes backwards
            for (var i = root.ChildNodes.Count - 1; i >= 0; i--)
            {
                nodes.Push(root.ChildNodes[i]);
            }
            // Loop through everything on the stack
            while (nodes.Count > 0)
            {
                var currentNode = nodes.Pop();
                var markersToInsert = ParseMarker(currentNode);
                markers.AddRange(markersToInsert);
                if (!currentNode.HasChildNodes)
                {
                    continue;
                }
                
                // If current node has children add them backwards
                for (var i = currentNode.ChildNodes.Count - 1; i >= 0; i--)
                {
                    nodes.Push(currentNode.ChildNodes[i]);
                }
            }

            var output = new USFMDocument();
            output.InsertMultiple(markers);
            return output;
        }

        private List<Marker> ParseMarker(XmlNode node)
        {
            var output = new List<Marker>();
            
            // if the node's style is in the list of ignored tags then return an empty list
            if (node.Attributes != null &&  _tagsToIgnore.Contains(node.Attributes["style"]?.Value))
            {
                return output;
            }
            
            switch (node.LocalName)
            {
                case "#text":
                    if (node.InnerText == "")
                    {
                        break;
                    }
                    output.Add(new TextBlock(node.InnerText));
                    break;
                case "verse":
                    if (node.Attributes["number"] == null)
                    {
                        break;
                    }
                    output.Add(new VMarker()
                    {
                        VerseNumber = node.Attributes["number"].Value
                    });
                    break;
                case "para" or "book" or "char":
                    var marker = CommonParserHelpers.SelectMarker(node.Attributes["style"].Value);
                    if (node.FirstChild?.LocalName == "#text")
                    {
                        var resultingText = marker.PreProcess(node.FirstChild.InnerText);
                        node.FirstChild.InnerText = resultingText;
                    }
                    output.Add(marker);
                    break;
                case "note":
                    var noteMarker = CommonParserHelpers.SelectMarker(node.Attributes["style"].Value);
                    // If the selected marker is FMarker then grab the caller from the element
                    if (noteMarker is FMarker fMarker)
                    {
                        var caller = node.Attributes["caller"]?.Value;
                        fMarker.FootNoteCaller = caller;
                    }
                    output.Add(noteMarker);
                    break;
                case "chapter":
                    if (node.Attributes["number"] == null)
                    {
                        break;
                    }
                    output.Add(new CMarker()
                    {
                        Number = int.TryParse(node.Attributes["number"].Value, out var result) ? result : 0
                    });
                    break;
                case "ref":
                    // Callers aren't in the spec for USX but they are in the spec for USFM so we're going to hardcode
                    var xMarker = new XMarker()
                    {
                        CrossRefCaller = "+"
                    };
                    if (node.Attributes["loc"] != null)
                    {
                        xMarker.Contents.Add(new XOMarker() { OriginRef = node.Attributes["loc"].Value });
                    }
                    
                    // If the node has text then add an xq marker
                    if (node.FirstChild?.LocalName == "#text")
                    {
                        xMarker.Contents.Add(new XQMarker());
                    }
                    output.Add(xMarker);
                    break;
                case "table":
                    // Add a table to the output
                    output.Add(new TableBlock());
                    break;
                case "row":
                    // Add row to the output
                    output.Add(new TRMarker());
                    break;
                case "cell":
                    
                    // Find the correct node and insert it
                    if (node.Attributes["style"] == null)
                    {
                        break;
                    }
                    
                    var cellMarker = CommonParserHelpers.SelectMarker(node.Attributes["style"].Value);
                    if (cellMarker is not UnknownMarker)
                    {
                        output.Add(cellMarker);
                    }
                    break;
                default:
                    Console.WriteLine($"Unknown node type: {node.LocalName}");
                    break;
            }

            if (node.ParentNode != null && node.ParentNode.LastChild == node)
            {
                if (node.ParentNode.Attributes["style"] != null)
                {
                    var possibleEndMarker = CommonParserHelpers.SelectMarker($"{node.ParentNode.Attributes["style"].Value}*");
                    if (possibleEndMarker is not UnknownMarker)
                    {
                        output.Add(possibleEndMarker);
                    }
                }
            }
            return output;
        }

        }
}