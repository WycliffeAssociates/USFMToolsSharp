using System;
using System.Collections.Generic;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    class MissingEndMarkers : ILinterModule
    {
        public Dictionary<Type, Type> markerPairs;
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> missingEndMarkers = new List<LinterResult>();
            markerPairs = new Dictionary<Type, Type>
            {
                {typeof(ADDMarker),typeof(ADDEndMarker)},
                {typeof(BDMarker),typeof(BDEndMarker)},
                {typeof(BDITMarker),typeof(BDITEndMarker)},
                {typeof(BKMarker),typeof(BKEndMarker)},
                {typeof(CAMarker),typeof(CAEndMarker)},
                {typeof(EMMarker),typeof(EMEndMarker)},
                {typeof(FQAMarker),typeof(FQAEndMarker)},
                {typeof(FQMarker),typeof(FQEndMarker)},
                {typeof(FTMarker),typeof(FTEndMarker)},
                {typeof(FVMarker),typeof(FVEndMarker)},
                {typeof(IORMarker),typeof(IOREndMarker)},
                {typeof(ITMarker),typeof(ITEndMarker)},
                {typeof(NDMarker),typeof(NDEndMarker)},
                {typeof(NOMarker),typeof(NOEndMarker)},
                {typeof(QACMarker),typeof(QACEndMarker)},
                {typeof(QSMarker),typeof(QSEndMarker)},
                {typeof(RQMarker),typeof(RQEndMarker)},
                {typeof(SCMarker),typeof(SCEndMarker)},
                {typeof(SUPMarker),typeof(SUPEndMarker)},
                {typeof(TLMarker),typeof(TLEndMarker)},
                {typeof(VAMarker),typeof(VAEndMarker)},
                {typeof(VPMarker),typeof(VPEndMarker)},
                {typeof(WMarker),typeof(WEndMarker)},
                {typeof(XMarker), typeof(XEndMarker)},
            };

            foreach (Marker marker in input.Contents)
            {
                missingEndMarkers.AddRange(CheckChildMarkers(marker, input));
            }
            return missingEndMarkers;

        }
        /// <summary>
        /// Iterates through all children markers
        /// </summary>
        /// <param name="input"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<LinterResult> CheckChildMarkers(Marker input,USFMDocument root)
        {
            List<LinterResult> results = new List<LinterResult>();

            foreach(Marker marker in input.Contents)
            {
                if (markerPairs.ContainsKey(marker.GetType()))
                {
                    results.AddRange(CheckEndMarker(marker, root));
                }
                results.AddRange(CheckChildMarkers(marker, root));
            }
            return results;
        }
        /// <summary>
        /// Checks Closing Marker for Unique Marker 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<LinterResult> CheckEndMarker(Marker input,USFMDocument root)
        {
            List<int> markerPositions = new List<int>();
            List<Marker> hierarchy = root.GetHierarchyToMarker(input);
            List<Marker> siblingMarkers = hierarchy[hierarchy.Count - 2].Contents;
            foreach (Marker sibling in siblingMarkers)
            {
                if (sibling.GetType() == input.GetType())
                {
                    markerPositions.Add(sibling.Position);
                }
                else if (sibling.GetType() == markerPairs[input.GetType()])
                {
                    if (markerPositions.Count > 0)
                    {
                        markerPositions.RemoveAt(markerPositions.Count - 1);
                    }
                }
            }
            List<LinterResult> results = new List<LinterResult>();
            foreach(int loneMarkerPosition in markerPositions)
            {
                results.Add(new LinterResult
                {
                    Position = loneMarkerPosition,
                    Level = LinterLevel.Error,
                    Message = $"Missing Closing marker for {input.GetType().Name}"
                });

            }
            return results;

        }
    }

}
