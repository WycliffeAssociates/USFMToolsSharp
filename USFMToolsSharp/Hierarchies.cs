using System;
using System.Collections.Generic;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharp
{
    public static class Hierarchies
    {
        public static readonly Dictionary<Type, List<Type>> Original = new()
        {

            [typeof(ADDMarker)] = new() {typeof(TextBlock)},
            [typeof(BDITMarker)] = new() {typeof(TextBlock)},
            [typeof(BDMarker)] = new() {typeof(TextBlock)},
            [typeof(CLSMarker)] = new() {typeof(TextBlock)},
            [typeof(CMarker)] = new() {typeof(MMarker),typeof(MSMarker),typeof(SMarker),typeof(BMarker),typeof(DMarker),typeof(VMarker),typeof(PMarker),typeof(PCMarker),typeof(CDMarker),typeof(CPMarker),typeof(DMarker),typeof(CLMarker),typeof(QMarker),typeof(QSMarker),typeof(QSEndMarker),typeof(QAMarker),typeof(QMarker),typeof(NBMarker),typeof(RMarker),typeof(LIMarker),typeof(TableBlock),typeof(MMarker),typeof(MIMarker),typeof(PIMarker),typeof(CAMarker),typeof(CAEndMarker),typeof(SPMarker),typeof(TextBlock),typeof(REMMarker),typeof(DMarker),typeof(VAMarker),typeof(VAEndMarker),typeof(FMarker),typeof(FEndMarker),typeof(PMMarker),typeof(LHMarker),typeof(XMarker),typeof(XEndMarker),typeof(XTMarker)},
            [typeof(DMarker)] = new() {typeof(FMarker),typeof(FEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(TextBlock)},
            [typeof(EMMarker)] = new() {typeof(TextBlock)},
            [typeof(FMarker)] = new() {typeof(FRMarker),typeof(FREndMarker),typeof(FKMarker),typeof(FTMarker),typeof(FVMarker),typeof(FVEndMarker),typeof(FPMarker),typeof(FQAMarker),typeof(FQAEndMarker),typeof(FQMarker),typeof(FQEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker),typeof(TextBlock),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(XMarker),typeof(XEndMarker),typeof(XTMarker)},
            [typeof(FPMarker)] = new() {typeof(TextBlock)},
            [typeof(FQAMarker)] = new() {typeof(TextBlock),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(FQMarker)] = new() {typeof(TextBlock),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(FTMarker)] = new() {typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker),typeof(TextBlock),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(XMarker),typeof(XEndMarker)},
            [typeof(ILIMarker)] = new() {typeof(TextBlock)},
            [typeof(IMIMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker)},
            [typeof(IMMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker)},
            [typeof(IMQMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker)},
            [typeof(IOMarker)] = new() {typeof(TextBlock),typeof(IORMarker),typeof(IOREndMarker)},
            [typeof(IORMarker)] = new() {typeof(TextBlock)},
            [typeof(IPIMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(IPMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(IEMarker),typeof(RQMarker),typeof(RQEndMarker)},
            [typeof(IPQMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(IPRMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(IQMarker)] = new() {typeof(TextBlock)},
            [typeof(ITMarker)] = new() {typeof(TextBlock)},
            [typeof(KMarker)] = new() {typeof(TextBlock)},
            [typeof(LFMarker)] = new() {typeof(TextBlock)},
            [typeof(LHMarker)] = new() {typeof(TextBlock)},
            [typeof(LIKMarker)] = new() {typeof(TextBlock)},
            [typeof(LIMarker)] = new() {typeof(VMarker),typeof(TextBlock)},
            [typeof(LITLMarker)] = new() {typeof(TextBlock)},
            [typeof(LIVMarker)] = new() {typeof(TextBlock)},
            [typeof(MIMarker)] = new() {typeof(TextBlock),typeof(VMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(MMarker)] = new() {typeof(VMarker),typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(MRMarker)] = new() {typeof(FMarker),typeof(FEndMarker)},
            [typeof(MSMarker)] = new() {typeof(MRMarker),typeof(XMarker),typeof(XEndMarker)},
            [typeof(NDMarker)] = new() {typeof(TextBlock)},
            [typeof(NOMarker)] = new() {typeof(TextBlock)},
            [typeof(ORDMarker)] = new() {typeof(TextBlock)},
            [typeof(PCMarker)] = new() {typeof(VMarker),typeof(BMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker)},
            [typeof(PIMarker)] = new() {typeof(VMarker),typeof(BMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker)},
            [typeof(PMarker)] = new() {typeof(VMarker),typeof(BMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker),typeof(XMarker),typeof(LIMarker)},
            [typeof(PMCMarker)] = new() {typeof(TextBlock)},
            [typeof(PMMarker)] = new() {typeof(VMarker),typeof(BMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker),typeof(XMarker)},
            [typeof(PMOMarker)] = new() {typeof(TextBlock)},
            [typeof(PMRMarker)] = new() {typeof(TextBlock)},
            [typeof(PNGMarker)] = new() {typeof(TextBlock)},
            [typeof(PNMarker)] = new() {typeof(TextBlock)},
            [typeof(PRMarker)] = new() {typeof(TextBlock)},
            [typeof(PROMarker)] = new() {typeof(TextBlock)},
            [typeof(QAMarker)] = new() {typeof(QACMarker),typeof(QACEndMarker)},
            [typeof(QCMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QDMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QMarker)] = new() {typeof(BMarker),typeof(QSMarker),typeof(QSEndMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker),typeof(VMarker)},
            [typeof(QMMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QRMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QSMarker)] = new() {typeof(TextBlock)},
            [typeof(QTMarker)] = new() {typeof(TextBlock)},
            [typeof(RBMarker)] = new() {typeof(TextBlock)},
            [typeof(RMarker)] = new() {typeof(TextBlock)},
            [typeof(RQMarker)] = new() {typeof(TextBlock)},
            [typeof(SCMarker)] = new() {typeof(TextBlock)},
            [typeof(SIGMarker)] = new() {typeof(TextBlock)},
            [typeof(SLSMarker)] = new() {typeof(TextBlock)},
            [typeof(SMarker)] = new() {typeof(RMarker),typeof(FMarker),typeof(FEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(EMMarker),typeof(EMEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(TextBlock)},
            [typeof(SUPMarker)] = new() {typeof(TextBlock)},
            [typeof(TableBlock)] = new() {typeof(TRMarker)},
            [typeof(TCMarker)] = new() {typeof(TextBlock)},
            [typeof(TCRMarker)] = new() {typeof(TextBlock)},
            [typeof(THMarker)] = new() {typeof(TextBlock)},
            [typeof(THRMarker)] = new() {typeof(TextBlock)},
            [typeof(TLMarker)] = new() {typeof(TextBlock)},
            [typeof(TRMarker)] = new() {typeof(TCMarker),typeof(THMarker),typeof(TCRMarker),typeof(THRMarker)},
            [typeof(USFMDocument)] = new() {typeof(HMarker),typeof(IDEMarker),typeof(IDMarker),typeof(IBMarker),typeof(IQMarker),typeof(ILIMarker),typeof(IOTMarker),typeof(IOMarker),typeof(STSMarker),typeof(USFMMarker),typeof(TOC1Marker),typeof(TOC2Marker),typeof(TOC3Marker),typeof(TOCA1Marker),typeof(TOCA2Marker),typeof(TOCA3Marker),typeof(ISMarker),typeof(MTMarker),typeof(IMTMarker),typeof(IPMarker),typeof(IPIMarker),typeof(IMMarker),typeof(IMIMarker),typeof(IPQMarker),typeof(IMQMarker),typeof(IPRMarker),typeof(CLMarker),typeof(CMarker)},
            [typeof(VMarker)] = new() {typeof(VPMarker),typeof(VPEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(ADDMarker),typeof(ADDEndMarker),typeof(BMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(EMMarker),typeof(EMEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(QMarker),typeof(MMarker),typeof(FMarker),typeof(FEndMarker),typeof(FRMarker),typeof(FREndMarker),typeof(SPMarker),typeof(TextBlock),typeof(WMarker),typeof(WEndMarker),typeof(XMarker),typeof(XEndMarker),typeof(CLSMarker),typeof(RQMarker),typeof(RQEndMarker),typeof(PIMarker),typeof(MIMarker),typeof(QSMarker),typeof(QSEndMarker),typeof(QRMarker),typeof(QCMarker),typeof(QDMarker),typeof(QACMarker),typeof(QACEndMarker),typeof(SMarker),typeof(VAMarker),typeof(VAEndMarker),typeof(KMarker),typeof(KEndMarker),typeof(LFMarker),typeof(LIKMarker),typeof(LIKEndMarker),typeof(LITLMarker),typeof(LITLEndMarker),typeof(LIVMarker),typeof(LIMarker),typeof(LIVEndMarker),typeof(ORDMarker),typeof(ORDEndMarker),typeof(PMCMarker),typeof(PMOMarker),typeof(PMRMarker),typeof(PNMarker),typeof(PNEndMarker),typeof(PNGMarker),typeof(PNGEndMarker),typeof(PRMarker),typeof(QTMarker),typeof(QTEndMarker),typeof(RBMarker),typeof(RBEndMarker),typeof(SIGMarker),typeof(SIGEndMarker),typeof(SLSMarker),typeof(SLSEndMarker),typeof(WAMarker),typeof(WAEndMarker),typeof(WGMarker),typeof(WGEndMarker),typeof(WHMarker),typeof(WHEndMarker),typeof(WJMarker),typeof(WJEndMarker),typeof(FIGMarker),typeof(FIGEndMarker),typeof(PNMarker),typeof(PNEndMarker),typeof(PROMarker),typeof(PROEndMarker),typeof(REMMarker),typeof(PMarker),typeof(LIMarker),typeof(XTMarker)},
            [typeof(WAMarker)] = new() {typeof(TextBlock)},
            [typeof(WGMarker)] = new() {typeof(TextBlock)},
            [typeof(WHMarker)] = new() {typeof(TextBlock)},
            [typeof(WJMarker)] = new() {typeof(TextBlock)},
            [typeof(XMarker)] = new() {typeof(XOMarker),typeof(XTMarker),typeof(XQMarker),typeof(TextBlock)},
            [typeof(XOMarker)] = new() {typeof(TextBlock)},
            [typeof(XQMarker)] = new() {typeof(TextBlock)},
            [typeof(XTMarker)] = new() {typeof(TextBlock)},

        };
        public static readonly Dictionary<Type, List<Type>> ParagraphDominant = new()
        {
            [typeof(ADDMarker)] = new() {typeof(TextBlock)},
            [typeof(BDITMarker)] = new() {typeof(TextBlock)},
            [typeof(BDMarker)] = new() {typeof(TextBlock)},
            [typeof(CLSMarker)] = new() {typeof(TextBlock)},
            [typeof(CMarker)] = new() {typeof(CDMarker),typeof(CPMarker), typeof(CLMarker)},
            [typeof(DMarker)] = new() {typeof(FMarker),typeof(FEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(TextBlock)},
            [typeof(EMMarker)] = new() {typeof(TextBlock)},
            [typeof(FMarker)] = new() {typeof(FRMarker),typeof(FREndMarker),typeof(FKMarker),typeof(FTMarker),typeof(FVMarker),typeof(FVEndMarker),typeof(FPMarker),typeof(FQAMarker),typeof(FQAEndMarker),typeof(FQMarker),typeof(FQEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker),typeof(TextBlock),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(XMarker),typeof(XEndMarker),typeof(XTMarker)},
            [typeof(FPMarker)] = new() {typeof(TextBlock)},
            [typeof(FQAMarker)] = new() {typeof(TextBlock),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(FQMarker)] = new() {typeof(TextBlock),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(FTMarker)] = new() {typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker),typeof(TextBlock),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(XMarker),typeof(XEndMarker)},
            [typeof(ILIMarker)] = new() {typeof(TextBlock)},
            [typeof(IMIMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker)},
            [typeof(IMMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker)},
            [typeof(IMQMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker)},
            [typeof(IOMarker)] = new() {typeof(TextBlock),typeof(IORMarker),typeof(IOREndMarker)},
            [typeof(IORMarker)] = new() {typeof(TextBlock)},
            [typeof(IPIMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(IPMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(IEMarker),typeof(RQMarker),typeof(RQEndMarker)},
            [typeof(IPQMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(IPRMarker)] = new() {typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(IQMarker)] = new() {typeof(TextBlock)},
            [typeof(ITMarker)] = new() {typeof(TextBlock)},
            [typeof(KMarker)] = new() {typeof(TextBlock)},
            [typeof(LFMarker)] = new() {typeof(TextBlock)},
            [typeof(LHMarker)] = new() {typeof(TextBlock)},
            [typeof(LIKMarker)] = new() {typeof(TextBlock)},
            [typeof(LIMarker)] = new() {typeof(VMarker),typeof(TextBlock)},
            [typeof(LITLMarker)] = new() {typeof(TextBlock)},
            [typeof(LIVMarker)] = new() {typeof(TextBlock)},
            [typeof(MIMarker)] = new() {typeof(TextBlock),typeof(VMarker),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(MMarker)] = new() {typeof(VMarker),typeof(TextBlock),typeof(BKMarker),typeof(BKEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NDMarker),typeof(NDEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker)},
            [typeof(MRMarker)] = new() {typeof(FMarker),typeof(FEndMarker)},
            [typeof(MSMarker)] = new() {typeof(MRMarker),typeof(XMarker),typeof(XEndMarker)},
            [typeof(NDMarker)] = new() {typeof(TextBlock)},
            [typeof(NOMarker)] = new() {typeof(TextBlock)},
            [typeof(ORDMarker)] = new() {typeof(TextBlock)},
            [typeof(PCMarker)] = new() {typeof(VMarker),typeof(BMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker)},
            [typeof(PIMarker)] = new() {typeof(VMarker),typeof(BMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker)},
            [typeof(PMarker)] = new() {typeof(VMarker), typeof(CMarker), typeof(BMarker),typeof(SPMarker), typeof(SMarker), typeof(ITMarker), typeof(ITEndMarker), typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker),typeof(XMarker),typeof(LIMarker), typeof(MMarker)},
            [typeof(PMCMarker)] = new() {typeof(TextBlock)},
            [typeof(PMMarker)] = new() {typeof(VMarker),typeof(BMarker), typeof(QMarker),typeof(SPMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(LIMarker),typeof(QMarker),typeof(XMarker)},
            [typeof(PMOMarker)] = new() {typeof(TextBlock)},
            [typeof(PMRMarker)] = new() {typeof(TextBlock)},
            [typeof(PNGMarker)] = new() {typeof(TextBlock)},
            [typeof(PNMarker)] = new() {typeof(TextBlock)},
            [typeof(PRMarker)] = new() {typeof(TextBlock)},
            [typeof(PROMarker)] = new() {typeof(TextBlock)},
            [typeof(QAMarker)] = new() {typeof(QACMarker),typeof(QACEndMarker)},
            [typeof(QCMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QDMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QMarker)] = new() {typeof(BMarker),typeof(QSMarker),typeof(QSEndMarker),typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker),typeof(VMarker)},
            [typeof(QMMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QRMarker)] = new() {typeof(TextBlock),typeof(FMarker),typeof(FEndMarker),typeof(TLMarker),typeof(TLEndMarker),typeof(WMarker),typeof(WEndMarker)},
            [typeof(QSMarker)] = new() {typeof(TextBlock)},
            [typeof(QTMarker)] = new() {typeof(TextBlock)},
            [typeof(RBMarker)] = new() {typeof(TextBlock)},
            [typeof(RMarker)] = new() {typeof(TextBlock)},
            [typeof(RQMarker)] = new() {typeof(TextBlock)},
            [typeof(SCMarker)] = new() {typeof(TextBlock)},
            [typeof(SIGMarker)] = new() {typeof(TextBlock)},
            [typeof(SLSMarker)] = new() {typeof(TextBlock)},
            [typeof(SMarker)] = new() {typeof(RMarker),typeof(FMarker),typeof(FEndMarker),typeof(SCMarker),typeof(SCEndMarker),typeof(EMMarker),typeof(EMEndMarker),typeof(BDMarker),typeof(BDEndMarker),typeof(ITMarker),typeof(ITEndMarker),typeof(BDITMarker),typeof(BDITEndMarker),typeof(NOMarker),typeof(NOEndMarker),typeof(SUPMarker),typeof(SUPEndMarker),typeof(TextBlock)},
            [typeof(SUPMarker)] = new() {typeof(TextBlock)},
            [typeof(TableBlock)] = new() {typeof(TRMarker)},
            [typeof(TCMarker)] = new() {typeof(TextBlock)},
            [typeof(TCRMarker)] = new() {typeof(TextBlock)},
            [typeof(THMarker)] = new() {typeof(TextBlock)},
            [typeof(THRMarker)] = new() {typeof(TextBlock)},
            [typeof(TLMarker)] = new() {typeof(TextBlock)},
            [typeof(TRMarker)] = new() {typeof(TCMarker),typeof(THMarker),typeof(TCRMarker),typeof(THRMarker)},
            [typeof(USFMDocument)] = new() {typeof(HMarker),typeof(IDEMarker),typeof(IDMarker),typeof(IBMarker),typeof(IQMarker),typeof(ILIMarker),typeof(IOTMarker),typeof(IOMarker),typeof(STSMarker),typeof(USFMMarker),typeof(TOC1Marker),typeof(TOC2Marker),typeof(TOC3Marker),typeof(TOCA1Marker),typeof(TOCA2Marker),typeof(TOCA3Marker),typeof(ISMarker),typeof(MTMarker),typeof(IMTMarker),typeof(IPMarker),typeof(IPIMarker),typeof(IMMarker),typeof(IMIMarker),typeof(IPQMarker),typeof(IMQMarker),typeof(IPRMarker),typeof(CLMarker),typeof(CMarker)},
            [typeof(VMarker)] = new() {typeof(VPMarker)},
            [typeof(WAMarker)] = new() {typeof(TextBlock)},
            [typeof(WGMarker)] = new() {typeof(TextBlock)},
            [typeof(WHMarker)] = new() {typeof(TextBlock)},
            [typeof(WJMarker)] = new() {typeof(TextBlock)},
            [typeof(XMarker)] = new() {typeof(XOMarker),typeof(XTMarker),typeof(XQMarker),typeof(TextBlock)},
            [typeof(XOMarker)] = new() {typeof(TextBlock)},
            [typeof(XQMarker)] = new() {typeof(TextBlock)},
            [typeof(XTMarker)] = new() {typeof(TextBlock)},
        };
    }
}