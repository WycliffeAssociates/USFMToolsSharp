using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using USFMToolsSharp;
using USFMToolsSharp.Models.Markers;

namespace USFMToolsSharpTest
{

    [TestClass]
    public class USFMParserTest
    {
        private USFMToolsSharp.USFMParser parser;

        [TestInitialize]
        public void SetupTest()
        {
            parser = new USFMToolsSharp.USFMParser();
        }
        private void CheckTypeList(List<Type> types, List<Marker> markers)
        {
            for (var i = 0; i < types.Count; i++)
            {
                Assert.IsTrue(markers[i].GetType() == types[i], $"Expected type {types[i].Name} but got {markers[i].GetType().Name} at index {i}");
            }
        }

        [TestMethod]
        public void TestIgnoredTags()
        {
            parser = new USFMToolsSharp.USFMParser(new List<string> { "bd", "bd*" });
            USFMDocument doc = parser.ParseFromString("\\v 1 In the beginning \\bd God \\bd*");
            Assert.AreEqual(1, doc.Contents.Count);
            VMarker vm = (VMarker)doc.Contents[0];
            Assert.AreEqual(1, vm.Contents.Count);
            TextBlock tb = (TextBlock)vm.Contents[0];
            Assert.AreEqual(0, tb.Contents.Count);
            Assert.AreEqual("In the beginning ", tb.Text);
        }

        [TestMethod]
        public void TestIdentificationMarkers()
        {
            Assert.AreEqual("Genesis", ((IDMarker)parser.ParseFromString("\\id Genesis").Contents[0]).TextIdentifier);
            Assert.AreEqual("UTF-8",   ((IDEMarker)parser.ParseFromString("\\ide UTF-8").Contents[0]).Encoding);
            Assert.AreEqual("2",       ((STSMarker)parser.ParseFromString("\\sts 2").Contents[0]).StatusText);

            Assert.AreEqual("3.0", ((USFMMarker)parser.ParseFromString("\\usfm 3.0").Contents[0]).Version);

            USFMDocument doc = parser.ParseFromString("\\rem Remark");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(REMMarker));
            REMMarker rem = (REMMarker)doc.Contents[0];
            Assert.AreEqual("Remark", rem.Comment);
        }

        [TestMethod]
        public void TestIntroductionMarkers()
        {
            Assert.AreEqual("Title", ((IMTMarker)parser.ParseFromString("\\imt Title").Contents[0]).IntroTitle);
            Assert.AreEqual(1,       ((IMTMarker)parser.ParseFromString("\\imt").Contents[0]).Weight);
            Assert.AreEqual(1,       ((IMTMarker)parser.ParseFromString("\\imt1").Contents[0]).Weight);
            Assert.AreEqual(2,       ((IMTMarker)parser.ParseFromString("\\imt2").Contents[0]).Weight);
            Assert.AreEqual(3,       ((IMTMarker)parser.ParseFromString("\\imt3").Contents[0]).Weight);

            Assert.AreEqual("Heading", ((ISMarker)parser.ParseFromString("\\is Heading").Contents[0]).Heading);
            Assert.AreEqual(1,         ((ISMarker)parser.ParseFromString("\\is").Contents[0]).Weight);
            Assert.AreEqual(1,         ((ISMarker)parser.ParseFromString("\\is1").Contents[0]).Weight);
            Assert.AreEqual(2,         ((ISMarker)parser.ParseFromString("\\is2").Contents[0]).Weight);
            Assert.AreEqual(3,         ((ISMarker)parser.ParseFromString("\\is3").Contents[0]).Weight);

            Assert.AreEqual(1, ((IQMarker)parser.ParseFromString("\\iq").Contents[0]).Depth);
            Assert.AreEqual(1, ((IQMarker)parser.ParseFromString("\\iq1").Contents[0]).Depth);
            Assert.AreEqual(2, ((IQMarker)parser.ParseFromString("\\iq2").Contents[0]).Depth);
            Assert.AreEqual(3, ((IQMarker)parser.ParseFromString("\\iq3").Contents[0]).Depth);

            Assert.IsNotNull(((IBMarker)parser.ParseFromString("\\ib").Contents[0]));

            Assert.AreEqual("Title", ((IOTMarker)parser.ParseFromString("\\iot Title").Contents[0]).Title);

            Assert.AreEqual(1, ((IOMarker)parser.ParseFromString("\\io").Contents[0]).Depth);
            Assert.AreEqual(1, ((IOMarker)parser.ParseFromString("\\io1").Contents[0]).Depth);
            Assert.AreEqual(2, ((IOMarker)parser.ParseFromString("\\io2").Contents[0]).Depth);
            Assert.AreEqual(3, ((IOMarker)parser.ParseFromString("\\io3").Contents[0]).Depth);

            USFMDocument doc = parser.ParseFromString("\\ior (1.1-3)\\ior*");
            Assert.AreEqual(2, doc.Contents.Count);
            Assert.AreEqual("(1.1-3)", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            Assert.AreEqual("Text", ((TextBlock)parser.ParseFromString("\\ili Text").Contents[0].Contents[0]).Text);
            Assert.AreEqual(1,      ((ILIMarker)parser.ParseFromString("\\ili").Contents[0]).Depth);
            Assert.AreEqual(1,      ((ILIMarker)parser.ParseFromString("\\ili1").Contents[0]).Depth);
            Assert.AreEqual(2,      ((ILIMarker)parser.ParseFromString("\\ili2").Contents[0]).Depth);
            Assert.AreEqual(3,      ((ILIMarker)parser.ParseFromString("\\ili3").Contents[0]).Depth);

            doc = parser.ParseFromString("\\ip Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IPMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\ipi Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IPIMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\im Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IMMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\is Heading");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(ISMarker));
            Assert.AreEqual("Heading", ((ISMarker)doc.Contents[0]).Heading);

            doc = parser.ParseFromString("\\iq Quote");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IQMarker));
            Assert.AreEqual("Quote", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((IQMarker)parser.ParseFromString("\\iq Quote").Contents[0]).Depth);
            Assert.AreEqual(1, ((IQMarker)parser.ParseFromString("\\iq1 Quote").Contents[0]).Depth);
            Assert.AreEqual(2, ((IQMarker)parser.ParseFromString("\\iq2 Quote").Contents[0]).Depth);
            Assert.AreEqual(3, ((IQMarker)parser.ParseFromString("\\iq3 Quote").Contents[0]).Depth);

            doc = parser.ParseFromString("\\imi Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IMIMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\ipq Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IPQMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\imq Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IMQMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\ipr Text");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(IPRMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

        }

        [TestMethod]
        public void TestSectionParse()
        {
            // Section Headings
            Assert.AreEqual("Silsilah Yesus Kristus ", ((SMarker)parser.ParseFromString("\\s Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Text);
            Assert.AreEqual("Kumpulkanlah Harta di Surga ", ((SMarker)parser.ParseFromString("\\s3 Kumpulkanlah Harta di Surga \\r (Luk. 12:33 - 34; 11:34 - 36; 16:13)").Contents[0]).Text);
            Assert.AreEqual(1, ((SMarker)parser.ParseFromString("\\s Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Weight);
            Assert.AreEqual(2, ((SMarker)parser.ParseFromString("\\s2 Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Weight);
            Assert.AreEqual(3, ((SMarker)parser.ParseFromString("\\s3 Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Weight);
            
            // Major Section 
            Assert.AreEqual("jilid 1 ", ((MSMarker)parser.ParseFromString("\\ms1 jilid 1 \\mr (Mazmur 1 - 41)").Contents[0]).Heading);
            Assert.AreEqual("jilid 1 ", ((MSMarker)parser.ParseFromString("\\ms2 jilid 1 \\mr (Mazmur 1 - 41)").Contents[0]).Heading);
            Assert.AreEqual(3, ((MSMarker)parser.ParseFromString("\\ms3 jilid 1 \\mr (Mazmur 1 - 41)").Contents[0]).Weight);
            Assert.AreEqual(1, ((MSMarker)parser.ParseFromString("\\ms jilid 1 \\mr (Mazmur 1 - 41)").Contents[0]).Weight);
            
            // References
            Assert.AreEqual("(Mazmur 1 - 41)", ((MRMarker)parser.ParseFromString("\\ms2 jilid 1 \\mr (Mazmur 1 - 41)").Contents[0].Contents[0]).SectionReference);
            Assert.AreEqual("(Mazmur 41)", ((MRMarker)parser.ParseFromString("\\ms2 jilid 1 \\mr (Mazmur 41)").Contents[0].Contents[0]).SectionReference);
            Assert.AreEqual("(Mazmur)", ((MRMarker)parser.ParseFromString("\\ms2 jilid 1 \\mr (Mazmur)").Contents[0].Contents[0]).SectionReference);

        }
        [TestMethod]
        public void TestTableOfContentsParse()
        {
            // Table of Contents
            Assert.AreEqual("Keluaran", ((TOC1Marker)parser.ParseFromString("\\toc1 Keluaran").Contents[0]).LongTableOfContentsText);
            Assert.AreEqual("Keluaran", ((TOC2Marker)parser.ParseFromString("\\toc2 Keluaran").Contents[0]).ShortTableOfContentsText);
            Assert.AreEqual("Kel", ((TOC3Marker)parser.ParseFromString("\\toc3 Kel").Contents[0]).BookAbbreviation);
            // Alternate Table of Contents
            Assert.AreEqual("Keluaran", ((TOCA1Marker)parser.ParseFromString("\\toca1 Keluaran").Contents[0]).AltLongTableOfContentsText);
            Assert.AreEqual("Keluaran", ((TOCA2Marker)parser.ParseFromString("\\toca2 Keluaran").Contents[0]).AltShortTableOfContentsText);
            Assert.AreEqual("Kel", ((TOCA3Marker)parser.ParseFromString("\\toca3 Kel").Contents[0]).AltBookAbbreviation);
        }
        [TestMethod]
        public void TestMajorTitleParse()
        {
            Assert.AreEqual("Keluaran", ((MTMarker)parser.ParseFromString("\\mt1 Keluaran").Contents[0]).Title);
            Assert.AreEqual("Keluaran", ((MTMarker)parser.ParseFromString("\\mt3 Keluaran").Contents[0]).Title);
            Assert.AreEqual(1, ((MTMarker)parser.ParseFromString("\\mt Keluaran").Contents[0]).Weight);
            Assert.AreEqual(2, ((MTMarker)parser.ParseFromString("\\mt2 Keluaran").Contents[0]).Weight);
        }
        [TestMethod]
        public void TestHeaderParse()
        {
            Assert.AreEqual("Genesis",((HMarker)parser.ParseFromString("\\h Genesis").Contents[0]).HeaderText);
            Assert.AreEqual("", ((HMarker)parser.ParseFromString("\\h").Contents[0]).HeaderText);
            Assert.AreEqual("1 John", ((HMarker)parser.ParseFromString("\\h 1 John").Contents[0]).HeaderText);
            Assert.AreEqual("", ((HMarker)parser.ParseFromString("\\h   ").Contents[0]).HeaderText);

            USFMDocument doc = parser.ParseFromString("\\sp Job");
            SPMarker sp = (SPMarker)doc.Contents[0];
            Assert.AreEqual("Job", sp.Speaker);
        }
        [TestMethod]
        public void TestChapterParse()
        {
            Assert.AreEqual(1, ((CMarker)parser.ParseFromString("\\c 1").Contents[0]).Number);
            Assert.AreEqual(1000, ((CMarker)parser.ParseFromString("\\c 1000").Contents[0]).Number);
            Assert.AreEqual(0, ((CMarker)parser.ParseFromString("\\c 0").Contents[0]).Number);

            // Chapter Labels
            Assert.AreEqual("Chapter One", ((CLMarker)parser.ParseFromString("\\c 1 \\cl Chapter One").Contents[0].Contents[0]).Label);
            Assert.AreEqual("Chapter One", ((CLMarker)parser.ParseFromString("\\cl Chapter One \\c 1").Contents[0]).Label);
            Assert.AreEqual("Chapter Two", ((CLMarker)parser.ParseFromString("\\c 1 \\cl Chapter One \\c 2 \\cl Chapter Two").Contents[1].Contents[0]).Label);

            USFMDocument doc = parser.ParseFromString("\\cp Q");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(CPMarker));
            Assert.AreEqual("Q", ((CPMarker)doc.Contents[0]).PublishedChapterMarker);

            doc = parser.ParseFromString("\\ca 53 \\ca*");
            Assert.AreEqual(2, doc.Contents.Count);
            CAMarker caBegin = (CAMarker)doc.Contents[0];
            CAEndMarker caEnd = (CAEndMarker)doc.Contents[1];
            Assert.AreEqual("53", caBegin.AltChapterNumber);

            doc = parser.ParseFromString("\\va 22 \\va*");
            Assert.AreEqual(2, doc.Contents.Count);
            VAMarker vaBegin = (VAMarker)doc.Contents[0];
            VAEndMarker vaEnd = (VAEndMarker)doc.Contents[1];
            Assert.AreEqual("22", vaBegin.AltVerseNumber);

            doc = parser.ParseFromString("\\p In the beginning God created the heavens and the earth.");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(PMarker));
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\pc In the beginning God created the heavens and the earth.");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(PCMarker));
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\p \\v 1 In the beginning God created the heavens and the earth.");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(PMarker));
            PMarker pm = (PMarker)doc.Contents[0];
            VMarker vm = (VMarker)pm.Contents[0];
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)vm.Contents[0]).Text);

            doc = parser.ParseFromString("\\mi");
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.IsInstanceOfType(doc.Contents[0], typeof(MIMarker));

            doc = parser.ParseFromString("\\d A Psalm of David");
            Assert.AreEqual("A Psalm of David", ((DMarker)doc.Contents[0]).Description);

            doc = parser.ParseFromString("\\nb");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(NBMarker));

            doc = parser.ParseFromString("\\fq the Son of God");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(FQMarker));
            Assert.AreEqual("the Son of God", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\pi The one who scattered");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(PIMarker));
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.AreEqual("The one who scattered", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((PIMarker)parser.ParseFromString("\\pi").Contents[0]).Depth);
            Assert.AreEqual(1, ((PIMarker)parser.ParseFromString("\\pi1").Contents[0]).Depth);
            Assert.AreEqual(2, ((PIMarker)parser.ParseFromString("\\pi2").Contents[0]).Depth);
            Assert.AreEqual(3, ((PIMarker)parser.ParseFromString("\\pi3").Contents[0]).Depth);

            doc = parser.ParseFromString("\\m \\v 37 David himself called him 'Lord';");
            Assert.AreEqual(1, doc.Contents.Count);
            MMarker mm = (MMarker)doc.Contents[0];
            Assert.AreEqual(1, mm.Contents.Count);
            vm = (VMarker)mm.Contents[0];
            Assert.AreEqual("David himself called him 'Lord';", ((TextBlock)vm.Contents[0]).Text);

            doc = parser.ParseFromString("\\b");
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.IsInstanceOfType(doc.Contents[0], typeof(BMarker));
        }
        [TestMethod]
        public void TestVerseParse()
        {
            Assert.AreEqual("9", ((VMarker)parser.ParseFromString("\\v 9 Yahweh God called to the man and said to him, \"Where are you?\"").Contents[0]).VerseNumber);
            Assert.AreEqual("26", ((VMarker)(parser.ParseFromString("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*").Contents[0])).VerseNumber);
            Assert.AreEqual("0", ((VMarker)parser.ParseFromString("\\v 0 Not in the Bible").Contents[0]).VerseNumber);
            Assert.AreEqual("1-2", ((VMarker)parser.ParseFromString("\\v 1-2 Not in the Bible").Contents[0]).VerseNumber);
            Assert.AreEqual(1, ((VMarker)parser.ParseFromString("\\v 1-2 Not in the Bible").Contents[0]).StartingVerse);
            Assert.AreEqual(2, ((VMarker)parser.ParseFromString("\\v 1-2 Not in the Bible").Contents[0]).EndingVerse);

            // References - Quoted book title - Parallel passage reference
            Assert.AreEqual("(Luk. 3:23 - 38)", ((TextBlock)parser.ParseFromString("\\s Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("(Luk. 12:33 - 34; 11:34 - 36; 16:13)", ((TextBlock)parser.ParseFromString("\\s Kumpulkanlah Harta di Surga \\r (Luk. 12:33 - 34; 11:34 - 36; 16:13)").Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("Kitab Peperangan TUHAN,", ((BKMarker)parser.ParseFromString("\\v 14 Itulah sebabnya kata-kata ini ditulis dalam \\bk Kitab Peperangan TUHAN,\\bk*").Contents[0].Contents[1]).BookTitle);
            Assert.AreEqual("Psa 2.7", ((TextBlock)parser.ParseFromString("\\v 5 For God never said to any of his angels,\\q1 \"You are my Son;\\q2 today I have become your Father.\"\\rq Psa 2.7\\rq* ").Contents[0].Contents[3].Contents[0]).Text);

            // Closing - Selah
            Assert.AreEqual("[[ayt.co/Mat]]", ((TextBlock)parser.ParseFromString("\\cls [[ayt.co/Mat]]").Contents[0].Contents[0]).Text);
            Assert.AreEqual("Sela", ((TextBlock)parser.ParseFromString("\\v 3 Allah datang dari negeri Teman \\q2 dan Yang Mahakudus datang dari Gunung Paran. \\qs Sela \\qs* ").Contents[0].Contents[1].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Sela", ((TextBlock)parser.ParseFromString("\\q2 dan sampai batu yang penghabisan. \\qs Sela \\qs*").Contents[0].Contents[1].Contents[0]).Text);

            // Transliterated
            Assert.AreEqual("Hades", ((TextBlock)parser.ParseFromString("\\f + \\fr 10:15 \\fk dunia orang mati \\ft Dalam bahasa Yunani adalah \\tl Hades\\tl* \\ft , tempat orang setelah meninggal.\\f*").Contents[0].Contents[2].Contents[1].Contents[0]).Text);
            Assert.AreEqual("TEKEL", ((TextBlock)parser.ParseFromString("\\v 27 \\tl TEKEL\\tl* :").Contents[0].Contents[0].Contents[0]).Text);
        }
        [TestMethod]
        public void TestTableParse()
        {
            Assert.IsInstanceOfType(parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0], typeof(TableBlock));

            // Table Rows - Cells
            Assert.IsInstanceOfType(parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0], typeof(TRMarker));
            Assert.AreEqual("dari suku Ruben", ((TextBlock)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("12.000", ((TextBlock)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual(1, ((TCMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(2, ((TCMarker)parser.ParseFromString("\\tr \\tc2 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(3, ((TCMarker)parser.ParseFromString("\\tr \\tc3 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(1, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr1 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
            Assert.AreEqual(2, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
            Assert.AreEqual(3, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr3 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);

            // Test verses
            Assert.IsTrue(parser.ParseFromString("\\tc1 \\v 6 dari suku Asyer").Contents[1] is VMarker);

            // Table Headers
            Assert.AreEqual("dari suku Ruben", ((TextBlock)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("12.000", ((TextBlock)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual(1, ((THMarker)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(2, ((THMarker)parser.ParseFromString("\\tr \\th2 dari suku Ruben \\thr 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(3, ((THMarker)parser.ParseFromString("\\tr \\th3 dari suku Ruben \\thr 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);

            Assert.AreEqual(1, ((THRMarker)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr1 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
            Assert.AreEqual(2, ((THRMarker)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
            Assert.AreEqual(3, ((THRMarker)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr3 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
        }
        [TestMethod]
        public void TestListParse()
        {
            // List Items
            Assert.AreEqual("Peres ayah Hezron.", ((TextBlock)parser.ParseFromString("\\li Peres ayah Hezron.").Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((LIMarker)parser.ParseFromString("\\li Peres ayah Hezron.").Contents[0]).Depth);
            Assert.AreEqual(1, ((LIMarker)parser.ParseFromString("\\li1 Peres ayah Hezron.").Contents[0]).Depth);
            Assert.AreEqual(2, ((LIMarker)parser.ParseFromString("\\li2 Peres ayah Hezron.").Contents[0]).Depth);
            Assert.AreEqual(3, ((LIMarker)parser.ParseFromString("\\li3 Peres ayah Hezron.").Contents[0]).Depth);
            // Verse within List
            Assert.AreEqual("19", ((VMarker)parser.ParseFromString("\\li Peres ayah Hezron. \\li \\v 19 Hezron ayah Ram.").Contents[1].Contents[0]).VerseNumber);
        }
        [TestMethod]
        public void TestFootnoteParse()
        {
            // Footnote Text Marker
            Assert.AreEqual("Sample Simple Footnote. ", ((TextBlock)parser.ParseFromString("\\f + \\ft Sample Simple Footnote. \\f*").Contents[0].Contents[0].Contents[0]).Text);

            // Footnote Caller
            Assert.AreEqual("+", ((FMarker)parser.ParseFromString("\\f + \\ft Sample Simple Footnote. \\f*").Contents[0]).FootNoteCaller);
            Assert.AreEqual("-", ((FMarker)parser.ParseFromString("\\f - \\ft Sample Simple Footnote. \\f*").Contents[0]).FootNoteCaller);
            Assert.AreEqual("abc", ((FMarker)parser.ParseFromString("\\f abc \\ft Sample Simple Footnote. \\f*").Contents[0]).FootNoteCaller);

            // Footnote Alternate Translation Marker
            Assert.AreEqual("... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth ", ((TextBlock)parser.ParseFromString("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*").Contents[0].Contents[1].Contents[1].Contents[0]).Text);

            // Footnote Keyword
            Assert.AreEqual("Tamar", ((FKMarker)parser.ParseFromString("\\f + \\fr 1.3 \\fk Tamar \\ft Menantu Yehuda yang akhirnya menjadi istrinya (bc. Kej. 38:1-30).\\f*").Contents[0].Contents[1]).FootNoteKeyword);

            //Footnote Reference
            Assert.AreEqual("1.3", ((FRMarker)parser.ParseFromString("\\f + \\fr 1.3 \\fk Tamar \\ft Menantu Yehuda yang akhirnya menjadi istrinya (bc. Kej. 38:1-30).\\f*").Contents[0].Contents[0]).VerseReference);

            // Footnote Verse Marker - Paragraph
            Assert.AreEqual("56", ((FVMarker)parser.ParseFromString("\\f + \\fr 9:55 \\ft Beberapa salinan Bahasa Yunani menambahkan: Dan ia berkata, Kamu tidak tahu roh apa yang memilikimu. \\fv 56 \\fv* \\ft Anak Manusia tidak datang untuk menghancurkan hidup manusia, tetapi untuk menyelamatkan mereka.\\f*").Contents[0].Contents[2]).VerseCharacter);
            Assert.IsInstanceOfType(parser.ParseFromString("\\f + \\fr 17.25 \\ft Kemungkinan maksudnya adalah bebas dari kewajiban pajak seumur hidup. (bdk. NIV. NET) \\fp \\f*").Contents[0].Contents[2],typeof(FPMarker));

            // Make sure that a fqa end marker doesn't end up outside of the footnote
            Assert.AreEqual(1, parser.ParseFromString("\\v 1 Words \\f + \\fqa Thing \\fqa* \\f*").Contents.Count);
        }
        [TestMethod]
        public void TestCrossReferenceParse()
        {
            // Cross Reference Caller
            Assert.AreEqual("-", ((XMarker)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*").Contents[0]).CrossRefCaller);
           
            // Cross Reference Origin
            Assert.AreEqual("11.21", ((XOMarker)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*").Contents[0].Contents[0]).OriginRef);
            
            // Cross Reference Target
            Assert.AreEqual("Mrk 1.24; Luk 2.39; Jhn 1.45.", ((TextBlock)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt Mrk 1.24; Luk 2.39; Jhn 1.45.\\x*").Contents[0].Contents[2].Contents[0]).Text);
            
            // Cross Reference Quotation
            Assert.AreEqual("Tebes", ((TextBlock)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*").Contents[0].Contents[1].Contents[0]).Text);
        }
        [TestMethod]
        public void TestVerseCharacterParse()
        {
            Assert.AreEqual("1a", ((VPMarker)parser.ParseFromString("\\v 1 \\vp 1a \\vp* This is not Scripture").Contents[0].Contents[0]).VerseCharacter);
            Assert.AreEqual("2b", ((VPMarker)parser.ParseFromString("\\v 2 \\vp 2b \\vp* This is not Scripture").Contents[0].Contents[0]).VerseCharacter);
            Assert.AreEqual("asdf", ((VPMarker)parser.ParseFromString("\\v 1 \\vp asdf \\vp* This is not Scripture").Contents[0].Contents[0]).VerseCharacter);
        }
        [TestMethod]
        public void TestTranslationNotesParse()
        {
            // Translator’s addition
            Assert.AreEqual("dan mencari TUHAN semesta alam!", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi kepada penduduk kota yang lain sambil berkata,\\q2 'Mari kita pergi memohon belas kasihan TUHAN\\q1 \\add dan mencari TUHAN semesta alam!\\add * ").Contents[0].Contents[3].Contents[0]).Text);
            Assert.AreEqual("(malaikat)", ((TextBlock)parser.ParseFromString("\\v 1 “Pada tahun pertama pemerintahan Darius, orang Media, aku bangkit untuk menguatkan dan melindungi dia.” \\add (malaikat)\\add* dari Persia.").Contents[0].Contents[1].Contents[0]).Text);
        }
        [TestMethod]
        public void TestWordEntryParse()
        {
            // Within Footnotes
            Assert.AreEqual("Berhala", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w Berhala \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Term);
            
            // Word Entry Attributes
            Assert.AreEqual("Berhala", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w Berhala|Berhala \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Attributes["lemma"]);
            Assert.AreEqual("grace", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w gracious|lemma=\"grace\" \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Attributes["lemma"]);
            Assert.AreEqual("G5485", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w gracious|lemma=\"grace\" strong=\"G5485\" \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Attributes["strong"]);
            Assert.AreEqual("H1234,G5485", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w gracious|strong=\"H1234,G5485\" \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Attributes["strong"]);
            Assert.AreEqual("gnt5:51.1.2.1", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w gracious|lemma=\"grace\" srcloc=\"gnt5:51.1.2.1\" \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Attributes["srcloc"]);
            Assert.AreEqual("metadata", ((WMarker)parser.ParseFromString("\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w gracious|lemma=\"grace\" x-myattr=\"metadata\" srcloc=\"gnt5:51.1.2.1\" \\w* di Daftar Istilah.\\f*").Contents[0].Contents[2].Contents[1]).Attributes["x-myattr"]);
        }

        [TestMethod]
        public void TestUtf8WordEntryParse()
        {
            var parsed =
                parser.ParseFromString("\\w Δαυεὶδ|lemma=\"Δαυείδ\" strong=\"G11380\" x-morph=\"Gr,N,,,,,GMSI\"\\w*");
           Assert.AreEqual("Δαυεὶδ", ((WMarker)parsed.Contents[0]).Term); 
           Assert.AreEqual("Δαυείδ", ((WMarker)parsed.Contents[0]).Attributes["lemma"]); 
           Assert.AreEqual("G11380", ((WMarker)parsed.Contents[0]).Attributes["strong"]); 
           Assert.AreEqual("Gr,N,,,,,GMSI", ((WMarker)parsed.Contents[0]).Attributes["x-morph"]); 
        }
        
        
        [TestMethod]
        public void TestPoetryParse()
        {
            USFMDocument doc = parser.ParseFromString("\\q Quote");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(QMarker));
            Assert.AreEqual("Quote", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((QMarker)parser.ParseFromString("\\q Quote").Contents[0]).Depth);
            Assert.AreEqual(1, ((QMarker)parser.ParseFromString("\\q1 Quote").Contents[0]).Depth);
            Assert.AreEqual(2, ((QMarker)parser.ParseFromString("\\q2 Quote").Contents[0]).Depth);
            Assert.AreEqual(3, ((QMarker)parser.ParseFromString("\\q3 Quote").Contents[0]).Depth);

            doc = parser.ParseFromString("\\qr God's love never fails.");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(QRMarker));
            Assert.AreEqual("God's love never fails.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\qc Amen! Amen!");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(QCMarker));
            Assert.AreEqual("Amen! Amen!", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\qd For the director of music.");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(QDMarker));
            Assert.AreEqual("For the director of music.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\qac P\\qac*");
            Assert.AreEqual(2, doc.Contents.Count);
            QACMarker qac = (QACMarker)doc.Contents[0];
            QACEndMarker qacEnd = (QACEndMarker)doc.Contents[1];
            Assert.AreEqual("P", qac.AcrosticLetter);

            doc = parser.ParseFromString("\\qm God is on your side.");
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.IsInstanceOfType(doc.Contents[0], typeof(QMMarker));
            Assert.AreEqual("God is on your side.", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((QMMarker)parser.ParseFromString("\\qm God is on your side.").Contents[0]).Depth);
            Assert.AreEqual(1, ((QMMarker)parser.ParseFromString("\\qm1 God is on your side.").Contents[0]).Depth);
            Assert.AreEqual(2, ((QMMarker)parser.ParseFromString("\\qm2 God is on your side.").Contents[0]).Depth);
            Assert.AreEqual(3, ((QMMarker)parser.ParseFromString("\\qm3 God is on your side.").Contents[0]).Depth);

            doc = parser.ParseFromString("\\qa Aleph");
            Assert.IsInstanceOfType(doc.Contents[0], typeof(QAMarker));
            QAMarker qa = (QAMarker)doc.Contents[0];
            Assert.AreEqual("Aleph", qa.Heading);

        }
        [TestMethod]
        public void TestCharacterStylingParse()
        {
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\em Emphasis \\em* ").Contents[0].Contents[1],typeof(EMMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\bd Boldness \\bd* ").Contents[0].Contents[1],typeof(BDMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\bdit Boldness and Italics \\bdit* ").Contents[0].Contents[1],typeof(BDITMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\it Italics \\it* ").Contents[0].Contents[1],typeof(ITMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\sup Superscript \\sup* ").Contents[0].Contents[1],typeof(SUPMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\nd Name of Diety \\nd* ").Contents[0].Contents[1],typeof(NDMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\sc Small Caps \\sc* ").Contents[0].Contents[1],typeof(SCMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\no Normal \\no* ").Contents[0].Contents[1],typeof(NOMarker));
            
            // Text Content
            Assert.AreEqual("Emphasis", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\em Emphasis \\em* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Boldness", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\bd Boldness \\bd* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Boldness and Italics", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\bdit Boldness and Italics \\bdit* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Italics", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\it Italics \\it* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Superscript", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\sup Superscript \\sup* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Name of Diety", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\nd Name of Diety \\nd* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Small Caps", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\sc Small Caps \\sc* ").Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual("Normal", ((TextBlock)parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\no Normal \\no* ").Contents[0].Contents[1].Contents[0]).Text);

        }
        [TestMethod]
        public void TestUnknownMarkerParse()
        {
            Assert.AreEqual(" what is yy?", ((UnknownMarker)parser.ParseFromString("\\yy what is yy?").Contents[0]).ParsedValue);
            Assert.AreEqual("yy", ((UnknownMarker)parser.ParseFromString("\\yy what is yy?").Contents[0]).ParsedIdentifier);
            Assert.AreEqual(" what is z?", ((UnknownMarker)parser.ParseFromString("\\z what is z?").Contents[0]).ParsedValue);
            Assert.AreEqual("z", ((UnknownMarker)parser.ParseFromString("\\z what is z?").Contents[0]).ParsedIdentifier);
            Assert.AreEqual(" what is 1?", ((UnknownMarker)parser.ParseFromString("\\1 what is 1?").Contents[0]).ParsedValue);
            Assert.AreEqual("1", ((UnknownMarker)parser.ParseFromString("\\1  what is 1?").Contents[0]).ParsedIdentifier);
        }

        [TestMethod]
        public void TestWhitespacePreserve()
        {
            string verseText = "This is verse text ";
            string otherVerseText = " after the word";
            var output = parser.ParseFromString($"\\v 1 {verseText}\\bd Bold \\bd*{otherVerseText}");
            Assert.AreEqual(verseText, ((TextBlock)output.Contents[0].Contents[0]).Text);
            Assert.AreEqual(otherVerseText, ((TextBlock)output.Contents[0].Contents[3]).Text);
        }

        /// <summary>
        /// Verify that QMarker and VMarker nesting is handeld correctly
        /// </summary>
        [TestMethod]
        public void TestVersePoetryNesting()
        {
            string verseText = "\\q \\v 1 This is verse one \\q another poetry \\v 2 second verse";
            var output = parser.ParseFromString(verseText);
            Assert.AreEqual(2, output.Contents.Count);
            Assert.IsTrue(output.Contents[0] is QMarker);
            Assert.IsTrue(output.Contents[0].Contents[0] is VMarker);
            Assert.IsTrue(output.Contents[0].Contents[0].Contents[1] is QMarker);
            Assert.IsTrue(output.Contents[1] is VMarker);

            string secondVerseText = "\\v 1 This is verse one \\q another poetry \\v 2 second verse";

            output = parser.ParseFromString(secondVerseText);
            Assert.AreEqual(2, output.Contents.Count);
            Assert.IsTrue(output.Contents[0] is VMarker);
            Assert.IsTrue(output.Contents[0].Contents[1] is QMarker);
            Assert.IsTrue(output.Contents[1] is VMarker);
        }

        /// <summary>
        /// Verify that an empty QMarker gets pushed back out to being a block QMarker
        /// </summary>
        [TestMethod]
        public void TestEmptyQMarkerInVerse()
        {
            string verseText = "\\v 1 This is verse one \\q \\v 2 second verse";
            var output = parser.ParseFromString(verseText);
            Assert.AreEqual(2, output.Contents.Count);
            Assert.IsTrue(output.Contents[0] is VMarker);
            Assert.IsTrue(output.Contents[1] is QMarker qMarker && qMarker.IsPoetryBlock);
            Assert.IsTrue(output.Contents[1].Contents[0] is VMarker);
        }

        [TestMethod]
        public void TestBadChapterHandling()
        {
            string verseText = "\\c 1 Bad text here";
            var output = parser.ParseFromString(verseText);
            Assert.AreEqual(1, output.Contents.Count);
            Assert.IsTrue(output.Contents[0].Contents[0] is TextBlock);
            Assert.AreEqual(1, ((CMarker)output.Contents[0]).Number);
            Assert.AreEqual("Bad text here", ((TextBlock)output.Contents[0].Contents[0]).Text);
        }

        [TestMethod]
        public void TestNoChapterNumberingHandling()
        {
            string verseText = "\\c \\v 1 Bad text here";
            var output = parser.ParseFromString(verseText);
            Assert.AreEqual(1, output.Contents.Count);
            Assert.IsTrue(output.Contents[0] is CMarker);
            Assert.AreEqual(0, ((CMarker)output.Contents[0]).Number);
        }

        [TestMethod]
        public void TestNoChapterNumberingAndTextHandling()
        {
            string verseText = "\\c Text Block \\v 1 Bad text here";
            var output = parser.ParseFromString(verseText);
            Assert.AreEqual(1, output.Contents.Count);
            Assert.IsTrue(output.Contents[0] is CMarker);
            Assert.AreEqual(0, ((CMarker)output.Contents[0]).Number);
            Assert.AreEqual(2, output.Contents[0].Contents.Count);
            Assert.IsTrue(output.Contents[0].Contents[0] is TextBlock);
            Assert.AreEqual("Text Block", ((TextBlock)output.Contents[0].Contents[0]).Text);
        }

        [TestMethod]
        public void TestCorrectFQAEndMarkerNesting()
        {
            string verseText = "\\f + \\ft Text \\fqa Other \\fqa* More";
            var output = parser.ParseFromString(verseText);
            Assert.AreEqual(4, output.Contents[0].Contents.Count);
        }

        /// <summary>
        /// Verify that if a \q marker is at the end of a string it doesn't throw an exception
        /// </summary>
        [TestMethod]
        public void TestTrailingEmptyQMarker()
        {
            string verseText = "\\q";
            var output = parser.ParseFromString(verseText);
            Assert.IsTrue(output.Contents[0] is QMarker);
        }

        [TestMethod]
        public void TestIntroParagraphs()
        {
            string text = "\\ip \\rq \\rq* \\ie";
            var output = parser.ParseFromString(text);
            Assert.IsTrue(output.Contents[0] is IPMarker);
            Assert.IsTrue(output.Contents[0].Contents[0] is RQMarker);
            Assert.IsTrue(output.Contents[0].Contents[1] is RQEndMarker);
            Assert.IsTrue(output.Contents[0].Contents[3] is IEMarker);
        }

        [TestMethod]
        public void TestNewlineInTextBlock()
        {
            string verseText = @"This is text 
with a newline";
            string usfm = $"\\v 1 {verseText}";
            var output = parser.ParseFromString(usfm);
            Assert.IsTrue(output.Contents[0] is VMarker);
            Assert.IsTrue(output.Contents[0].Contents[0] is TextBlock);
            Assert.AreEqual(verseText, ((TextBlock)output.Contents[0].Contents[0]).Text);
        }

        [TestMethod]
        public void TestFigureParse()
        {
            //PRE 3.0 TESTS
            //Description;
            Assert.AreEqual("description", ((FIGMarker)parser.ParseFromString("\\fig description|filepath|width|location|copyright|caption caption caption|reference\\fig*").Contents[0]).Description);
            //FilePath;
            Assert.AreEqual("filepath", ((FIGMarker)parser.ParseFromString
            ("\\fig description| filepath|width|location|copyright|caption caption caption|reference\\fig*").Contents[0]).FilePath);
            //Width;
            Assert.AreEqual("width", ((FIGMarker)parser.ParseFromString
            ("\\fig description|filepath |width|location|copyright|caption caption caption|reference\\fig*").Contents[0]).Width);
            //Location;
            Assert.AreEqual("location", ((FIGMarker)parser.ParseFromString
            ("\\fig description|filepath|width | location|copyright|caption caption caption|reference\\fig*").Contents[0]).Location);
            //Copyright;
            Assert.AreEqual("copyright", ((FIGMarker)parser.ParseFromString
            ("\\fig description|filepath|width|location|copyright |caption caption caption|reference\\fig*").Contents[0]).Copyright);
            //Caption;
            Assert.AreEqual("caption caption caption", ((FIGMarker)parser.ParseFromString
            ("\\fig description|filepath|width|location|copyright|caption caption caption|reference\\fig*").Contents[0]).Caption);
            //Reference;
            Assert.AreEqual("reference", ((FIGMarker)parser.ParseFromString
            ("\\fig description|filepath|width|location|copyright|caption caption caption | reference\\fig*").Contents[0]).Reference);

            //3.0 TESTS
            //Caption;
            Assert.AreEqual("caption caption caption", ((FIGMarker)parser.ParseFromString
                ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
                 "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).Caption);
            //Description;
            Assert.AreEqual("description", ((FIGMarker)parser.ParseFromString
            ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
             "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).Description);
            //FilePath;
            Assert.AreEqual("filepath", ((FIGMarker)parser.ParseFromString
            ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
             "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).FilePath);
            //Width;
            Assert.AreEqual("width", ((FIGMarker)parser.ParseFromString
            ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
             "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).Width);
            //Location;
            Assert.AreEqual("location", ((FIGMarker)parser.ParseFromString
            ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
             "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).Location);
            //Copyright;
            Assert.AreEqual("copyright", ((FIGMarker)parser.ParseFromString
            ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
             "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).Copyright);
            //Reference;
            Assert.AreEqual("reference", ((FIGMarker)parser.ParseFromString
            ("\\fig caption caption caption | alt=\"description\" src=\"filepath\" size=\"width\" loc =\"location\" " +
             "copy= \"copyright\"  ref = \"reference\"\\fig*").Contents[0]).Reference);
            


            // Cross Reference Caller
            Assert.AreEqual("-", ((XMarker)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*").Contents[0]).CrossRefCaller);

            // Cross Reference Origin
            Assert.AreEqual("11.21", ((XOMarker)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*").Contents[0].Contents[0]).OriginRef);

            // Cross Reference Target
            Assert.AreEqual("Mrk 1.24; Luk 2.39; Jhn 1.45.", ((TextBlock)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt Mrk 1.24; Luk 2.39; Jhn 1.45.\\x*").Contents[0].Contents[2].Contents[0]).Text);

            // Cross Reference Quotation
            Assert.AreEqual("Tebes", ((TextBlock)parser.ParseFromString("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*").Contents[0].Contents[1].Contents[0]).Text);
        }
        [TestMethod]
        public void TestSpacingBetweenWords()
        {
            var parsed = parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\em Emphasis \\em* \\em Second \\em*");
            Assert.AreEqual(" ", ((TextBlock)parsed.Contents[0].Contents[3]).Text);
        }
        [TestMethod]
        public void TestIgnoreUnknownMarkers()
        {
            parser = new USFMParser(ignoreUnknownMarkers: true);
            var parsed = parser.ParseFromString("\\v 1 Text \\unknown more text \\bd Text \\bd*");
            Assert.AreEqual(1, parsed.Contents.Count);
            Assert.AreEqual(4, parsed.Contents[0].Contents.Count);
        }

        [TestMethod]
        public void TestIgnoreParentsWhenGettingChildMarkers()
        {
            var ignoredParents = new List<Type> { typeof(FMarker) };
            var result = parser.ParseFromString("\\v 1 Text blocks \\f \\ft Text \\f*");
            Assert.AreEqual(2, result.GetChildMarkers<TextBlock>().Count);
            Assert.AreEqual(1, result.GetChildMarkers<TextBlock>(ignoredParents).Count);
            var verse = result.GetChildMarkers<VMarker>()[0];
            Assert.AreEqual(2, verse.GetChildMarkers<TextBlock>().Count);
            Assert.AreEqual(1, verse.GetChildMarkers<TextBlock>(ignoredParents).Count);
            Assert.AreEqual(0, verse.GetChildMarkers<TextBlock>(new List<Type>() { typeof(VMarker)}).Count);
        }

        [TestMethod]
        public void TestGetChildMarkers()
        {
            var result = parser.ParseFromString("\\c 1 \\v 1 Text blocks \\f \\ft Text \\f* \\v 2 Third block \\c 2 \\v 1 Fourth block");
            var markers = result.GetChildMarkers<VMarker>();
            Assert.AreEqual(3, markers.Count);
            Assert.AreEqual("1", markers[0].VerseNumber);
            Assert.AreEqual("2", markers[1].VerseNumber);
            Assert.AreEqual("1", markers[2].VerseNumber);
        }

        [TestMethod]
        public void TestGetHierarchyToMarker()
        {
            var document = new USFMDocument();
            var chapter = new CMarker() { Number = 1 };
            var verse = new VMarker() { VerseNumber = "1" };
            var textblock = new TextBlock("Hello world");
            document.InsertMultiple(new Marker[] { chapter, verse, textblock });
            var result = document.GetHierarchyToMarker(textblock);
            Assert.AreEqual(document, result[0]);
            Assert.AreEqual(chapter, result[1]);
            Assert.AreEqual(verse, result[2]);
            Assert.AreEqual(textblock, result[3]);

            document = parser.ParseFromString(@"\c 1\p \v 1 Before \f + \ft In footnote \f* After footnore");

            var markers = document.GetChildMarkers<TextBlock>().ToArray();
            var baseMarker = document.Contents[0].Contents[0].Contents[0];
            var hierarchy = baseMarker.GetHierarchyToMarker(markers[0]).ToList();
            CheckTypeList(new List<Type> { typeof(VMarker), typeof(TextBlock) }, hierarchy);
            hierarchy = baseMarker.GetHierarchyToMarker(markers[1]).ToList();
            CheckTypeList(new List<Type> { typeof(VMarker), typeof(FMarker), typeof(FTMarker), typeof(TextBlock) }, hierarchy);
            hierarchy = baseMarker.GetHierarchyToMarker(markers[2]).ToList();
            CheckTypeList(new List<Type> { typeof(VMarker), typeof(TextBlock) }, hierarchy);

        }

        [TestMethod]
        public void TestGetHierarchyToMarkerWithNonExistantMarker()
        {
            var document = new USFMDocument();
            var chapter = new CMarker() { Number = 1 };
            var verse = new VMarker() { VerseNumber = "1" };
            var textblock = new TextBlock("Hello world");
            var secondBlock = new TextBlock("Hello again");
            document.InsertMultiple(new Marker[] { chapter, verse, textblock });
            var result = document.GetHierarchyToMarker(secondBlock);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestGetHierarchyToMultipleMarkers()
        {
            var document = new USFMDocument();
            var chapter = new CMarker() { Number = 1 };
            var verse = new VMarker() { VerseNumber = "1" };
            var textblock = new TextBlock("Hello world");
            var footnote = new FMarker();
            var footnoteText = new FTMarker();
            var footnoteEndMarker = new FEndMarker();
            var textInFootnote = new TextBlock("Text in footnote");
            var secondBlock = new TextBlock("Hello again");
            var nonExistant = new VMarker();
            var result = document.GetHierachyToMultipleMarkers(new List<Marker>());
            Assert.AreEqual(0, result.Count);
            result = document.GetHierachyToMultipleMarkers(new List<Marker>() {footnote});
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[footnote].Count);
            document.InsertMultiple(new Marker[] { chapter, verse, textblock, footnote, footnoteText, textInFootnote, footnoteEndMarker, secondBlock });
            result = document.GetHierachyToMultipleMarkers(new List<Marker>() { textblock, secondBlock, nonExistant, textInFootnote });
            Assert.AreEqual(document, result[textblock][0]);
            Assert.AreEqual(chapter, result[textblock][1]);
            Assert.AreEqual(verse, result[textblock][2]);
            Assert.AreEqual(textblock, result[textblock][3]);
            Assert.AreEqual(document, result[secondBlock][0]);
            Assert.AreEqual(chapter, result[secondBlock][1]);
            Assert.AreEqual(verse, result[secondBlock][2]);
            Assert.AreEqual(secondBlock, result[secondBlock][3]);
            Assert.AreEqual(0, result[nonExistant].Count);
            Assert.AreEqual(document, result[textInFootnote][0]);
            Assert.AreEqual(chapter, result[textInFootnote][1]);
            Assert.AreEqual(verse, result[textInFootnote][2]);
            Assert.AreEqual(footnote, result[textInFootnote][3]);
            Assert.AreEqual(footnoteText, result[textInFootnote][4]);
        }
    }
}
