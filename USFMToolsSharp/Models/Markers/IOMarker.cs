
namespace USFMToolsSharp.Models.Markers
{
    /// <summary>
    /// Introduction outline entry
    /// </summary>
    public class IOMarker : Marker
    {
        public int Depth = 1;
        public override string Identifier => "io";
    }
}
