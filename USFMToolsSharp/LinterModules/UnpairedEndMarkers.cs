using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USFMToolsSharp.Models;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp.LinterModules
{
    class UnpairedEndMarkers : ILinterModule
    {
        public Dictionary<Type, Type> markerPairs;
        public List<LinterResult> Lint(USFMDocument input)
        {
            List<LinterResult> missingEndMarkers = new List<LinterResult>();
            markerPairs = new Dictionary<Type, Type>
            {
                {typeof(ADDEndMarker),typeof(ADDMarker)},
                {typeof(BDEndMarker),typeof(BDMarker)},
                {typeof(BDITEndMarker),typeof(BDITMarker)},
                {typeof(BKEndMarker),typeof(BKMarker)},
                {typeof(CAEndMarker),typeof(CAMarker)},
                {typeof(EMEndMarker),typeof(EMMarker)},
                {typeof(FEndMarker),typeof(FMarker)},
                {typeof(FQAEndMarker),typeof(FQAMarker)},
                {typeof(FQEndMarker),typeof(FQMarker)},
                {typeof(FTEndMarker),typeof(FTMarker)},
                {typeof(FVEndMarker),typeof(FVMarker)},
                {typeof(IOREndMarker),typeof(IORMarker)},
                {typeof(ITEndMarker),typeof(ITMarker)},
                {typeof(NDEndMarker),typeof(NDMarker)},
                {typeof(NOEndMarker),typeof(NOMarker)},
                {typeof(QACEndMarker),typeof(QACMarker)},
                {typeof(QSEndMarker),typeof(QSMarker)},
                {typeof(RQEndMarker),typeof(RQMarker)},
                {typeof(SCEndMarker),typeof(SCMarker)},
                {typeof(SUPEndMarker),typeof(SUPMarker)},
                {typeof(TLEndMarker),typeof(TLMarker)},
                {typeof(VAEndMarker),typeof(VAMarker)},
                {typeof(VPEndMarker),typeof(VPMarker)},
                {typeof(WEndMarker),typeof(WMarker)},
                {typeof(XEndMarker), typeof(XMarker)},
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
        public List<LinterResult> CheckChildMarkers(Marker input, USFMDocument root)
        {
            List<LinterResult> results = new List<LinterResult>();

            foreach (Marker marker in input.Contents)
            {
                if (markerPairs.ContainsKey(marker.GetType()))
                {
                    results.AddRange(CheckOpenMarker(marker, root));
                }
                results.AddRange(CheckChildMarkers(marker, root));
            }
            return results;
        }
        /// <summary>
        /// Checks Opening Marker for Unique End Marker 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<LinterResult> CheckOpenMarker(Marker input, USFMDocument root)
        {
            List<int> markerPositions = new List<int>();
            List<Marker> hierarchy = root.GetHierarchyToMarker(input);
            List<Marker> siblingMarkers = new List<Marker>(hierarchy[hierarchy.Count - 2].Contents);
            siblingMarkers.Reverse();
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
            foreach (int loneMarkerPosition in markerPositions)
            {
                results.Add(new LinterResult
                {
                    Position = loneMarkerPosition,
                    Level = LinterLevel.Error,
                    Message = $"Missing Opening marker for {input.GetType().Name}"
                });

            }
            return results;

        }
    }

}