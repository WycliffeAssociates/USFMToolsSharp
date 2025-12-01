using System.Collections.Generic;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.Models;

public class HierachyNode
{
    public HierachyNode(Marker marker)
    {
        Marker = marker;
    }
    public Marker Marker { get; set; }
    public List<HierachyNode> Contents { get; set; } = new List<HierachyNode>();
    
    public HierachyNode this[int index]
    {
        get => Contents[index];
        set => Contents[index] = value;
    }
    
    public static implicit operator Marker(HierachyNode input) => input.Marker;
}