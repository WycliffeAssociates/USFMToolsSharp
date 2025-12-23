using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using USFMToolsSharp;
using USFMToolsSharp.Models;
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
        private static void CheckTypeList(List<Type> types, List<Marker> markers)
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
            var vm = doc.Contents[0];
            Assert.AreEqual(2, vm.Contents.Count);
            var tb = vm.Contents[0];
            Assert.AreEqual(0, tb.Contents.Count);
            Assert.AreEqual("In the beginning ", ((TextBlock)tb).Text);
            tb = vm.Contents[1];
            Assert.AreEqual("God ", ((TextBlock)tb).Text);
            
        }

        [TestMethod]
        public void TestIdentificationMarkers()
        {
            Assert.AreEqual("Genesis", ((IDMarker)parser.ParseFromString("\\id Genesis").Contents[0]).TextIdentifier);
            Assert.AreEqual("UTF-8",   ((IDEMarker)parser.ParseFromString("\\ide UTF-8").Contents[0]).Encoding);
            Assert.AreEqual("2",       ((STSMarker)parser.ParseFromString("\\sts 2").Contents[0]).StatusText);

            Assert.AreEqual("3.0", ((USFMMarker)parser.ParseFromString("\\usfm 3.0").Contents[0]).Version);

            USFMDocument doc = parser.ParseFromString("\\rem Remark");
            Assert.IsInstanceOfType(doc.Hierarchies[0][0].Marker, typeof(REMMarker));
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
            var hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IPMarker));
            Assert.AreEqual("Text", ((TextBlock)hierarchy[0][0]).Text);

            doc = parser.ParseFromString("\\ipi Text");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IPIMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\im Text");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IMMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\is Heading");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(ISMarker));
            Assert.AreEqual("Heading", ((ISMarker)doc.Contents[0]).Heading);

            doc = parser.ParseFromString("\\iq Quote");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IQMarker));
            Assert.AreEqual("Quote", ((TextBlock)hierarchy[0][0]).Text);
            Assert.AreEqual(1, ((IQMarker)parser.ParseFromString("\\iq Quote").Contents[0]).Depth);
            Assert.AreEqual(1, ((IQMarker)parser.ParseFromString("\\iq1 Quote").Contents[0]).Depth);
            Assert.AreEqual(2, ((IQMarker)parser.ParseFromString("\\iq2 Quote").Contents[0]).Depth);
            Assert.AreEqual(3, ((IQMarker)parser.ParseFromString("\\iq3 Quote").Contents[0]).Depth);

            doc = parser.ParseFromString("\\imi Text");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IMIMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\ipq Text");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IPQMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\imq Text");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IMQMarker));
            Assert.AreEqual("Text", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\ipr Text");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(IPRMarker));
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
            Assert.AreEqual(1, ((CMarker)parser.ParseFromString("\\c 1").Hierarchies[0][0]).Number);
            Assert.AreEqual(1000, ((CMarker)parser.ParseFromString("\\c 1000").Hierarchies[0][0]).Number);
            Assert.AreEqual(0, ((CMarker)parser.ParseFromString("\\c 0").Hierarchies[0][0]).Number);

            // Chapter Labels
            Assert.AreEqual("Chapter One", ((CLMarker)parser.ParseFromString("\\c 1 \\cl Chapter One").Contents[0][0]).Label);
            Assert.AreEqual("Chapter One", ((CLMarker)parser.ParseFromString("\\cl Chapter One \\c 1").Contents[0]).Label);
            Assert.AreEqual("Chapter Two", ((CLMarker)parser.ParseFromString("\\c 1 \\cl Chapter One \\c 2 \\cl Chapter Two").Contents[1].Contents[0]).Label);

            USFMDocument doc = parser.ParseFromString("\\cp Q");
            var hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(CPMarker));
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
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(PMarker));
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\pc In the beginning God created the heavens and the earth.");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(PCMarker));
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\p \\v 1 In the beginning God created the heavens and the earth.");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(PMarker));
            var pm = doc.Contents[0];
            var vm = pm.Contents[0];
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)vm.Contents[0]).Text);

            doc = parser.ParseFromString("\\mi");
            hierarchy = doc.Hierarchies[0];
            Assert.AreEqual(1, hierarchy.Contents.Count);
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(MIMarker));

            doc = parser.ParseFromString("\\d A Psalm of David");
            Assert.AreEqual("A Psalm of David", ((DMarker)doc.Contents[0]).Description);

            doc = parser.ParseFromString("\\nb");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(NBMarker));

            doc = parser.ParseFromString("\\fq the Son of God");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(FQMarker));
            Assert.AreEqual("the Son of God", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\pi The one who scattered");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(PIMarker));
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.AreEqual("The one who scattered", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((PIMarker)parser.ParseFromString("\\pi").Contents[0]).Depth);
            Assert.AreEqual(1, ((PIMarker)parser.ParseFromString("\\pi1").Contents[0]).Depth);
            Assert.AreEqual(2, ((PIMarker)parser.ParseFromString("\\pi2").Contents[0]).Depth);
            Assert.AreEqual(3, ((PIMarker)parser.ParseFromString("\\pi3").Contents[0]).Depth);

            doc = parser.ParseFromString("\\m \\v 37 David himself called him 'Lord';");
            Assert.AreEqual(1, doc.Contents.Count);
            var mm = doc.Contents[0];
            Assert.AreEqual(1, mm.Contents.Count);
            vm = mm.Contents[0];
            Assert.AreEqual("David himself called him 'Lord';", ((TextBlock)vm.Contents[0]).Text);

            doc = parser.ParseFromString("\\b");
            hierarchy = doc.Hierarchies[0];
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(BMarker));
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
            // Test with newline after the verse number
            Assert.AreEqual("1", ((VMarker)parser.ParseFromString("\\v 1\n").Contents[0]).VerseNumber);
            Assert.AreEqual("1", ((VMarker)parser.ParseFromString("\\v 1\"").Contents[0]).VerseNumber);
        }
        [TestMethod]
        public void TestTableParse()
        {
            Assert.IsInstanceOfType(parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Hierarchies[0][0].Marker, typeof(TableBlock));

            // Table Rows - Cells
            Assert.IsInstanceOfType(parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Hierarchies[0][0][0].Marker, typeof(TRMarker));
            Assert.AreEqual("dari suku Ruben", ((TextBlock)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("12.000", ((TextBlock)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual(1, ((TCMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(2, ((TCMarker)parser.ParseFromString("\\tr \\tc2 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(3, ((TCMarker)parser.ParseFromString("\\tr \\tc3 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(1, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr1 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
            Assert.AreEqual(2, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);
            Assert.AreEqual(3, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr3 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);

            // Test verses
            Assert.IsTrue(parser.ParseFromString("\\tc1 \\v 6 dari suku Asyer").Hierarchies[0][1].Marker is VMarker);

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
            Assert.IsInstanceOfType(parser.ParseFromString("\\f + \\fr 17.25 \\ft Kemungkinan maksudnya adalah bebas dari kewajiban pajak seumur hidup. (bdk. NIV. NET) \\fp \\f*").Hierarchies[0][0][2].Marker,typeof(FPMarker));

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
            var hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(QMarker));
            Assert.AreEqual("Quote", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((QMarker)parser.ParseFromString("\\q Quote").Contents[0]).Depth);
            Assert.AreEqual(1, ((QMarker)parser.ParseFromString("\\q1 Quote").Contents[0]).Depth);
            Assert.AreEqual(2, ((QMarker)parser.ParseFromString("\\q2 Quote").Contents[0]).Depth);
            Assert.AreEqual(3, ((QMarker)parser.ParseFromString("\\q3 Quote").Contents[0]).Depth);

            doc = parser.ParseFromString("\\qr God's love never fails.");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(QRMarker));
            Assert.AreEqual("God's love never fails.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\qc Amen! Amen!");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(QCMarker));
            Assert.AreEqual("Amen! Amen!", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\qd For the director of music.");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(QDMarker));
            Assert.AreEqual("For the director of music.", ((TextBlock)doc.Contents[0].Contents[0]).Text);

            doc = parser.ParseFromString("\\qac P\\qac*");
            Assert.AreEqual(2, doc.Contents.Count);
            QACMarker qac = (QACMarker)doc.Contents[0];
            QACEndMarker qacEnd = (QACEndMarker)doc.Contents[1];
            Assert.AreEqual("P", qac.AcrosticLetter);

            doc = parser.ParseFromString("\\qm God is on your side.");
            hierarchy = doc.Hierarchies[0];
            Assert.AreEqual(1, doc.Contents.Count);
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(QMMarker));
            Assert.AreEqual("God is on your side.", ((TextBlock)doc.Contents[0].Contents[0]).Text);
            Assert.AreEqual(1, ((QMMarker)parser.ParseFromString("\\qm God is on your side.").Contents[0]).Depth);
            Assert.AreEqual(1, ((QMMarker)parser.ParseFromString("\\qm1 God is on your side.").Contents[0]).Depth);
            Assert.AreEqual(2, ((QMMarker)parser.ParseFromString("\\qm2 God is on your side.").Contents[0]).Depth);
            Assert.AreEqual(3, ((QMMarker)parser.ParseFromString("\\qm3 God is on your side.").Contents[0]).Depth);

            doc = parser.ParseFromString("\\qa Aleph");
            hierarchy = doc.Hierarchies[0];
            Assert.IsInstanceOfType(hierarchy[0].Marker, typeof(QAMarker));
            QAMarker qa = (QAMarker)doc.Contents[0];
            Assert.AreEqual("Aleph", qa.Heading);

        }
        [TestMethod]
        public void TestCharacterStylingParse()
        {
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\em Emphasis \\em* ").Contents[0].Contents[1].Marker,typeof(EMMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\bd Boldness \\bd* ").Contents[0].Contents[1].Marker,typeof(BDMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\bdit Boldness and Italics \\bdit* ").Contents[0].Contents[1].Marker,typeof(BDITMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\it Italics \\it* ").Contents[0].Contents[1].Marker,typeof(ITMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\sup Superscript \\sup* ").Contents[0].Contents[1].Marker,typeof(SUPMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\nd Name of Diety \\nd* ").Contents[0].Contents[1].Marker,typeof(NDMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\sc Small Caps \\sc* ").Contents[0].Contents[1].Marker,typeof(SCMarker));
            Assert.IsInstanceOfType(parser.ParseFromString("\\v 21 Penduduk kota yang satu akan pergi \\no Normal \\no* ").Contents[0].Contents[1].Marker,typeof(NOMarker));
            
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
            Assert.AreEqual(" what is yy?", ((UnknownMarker)parser.ParseFromString("\\yy what is yy?").Hierarchies[0][0]).ParsedValue);
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
            Assert.IsTrue(output.Contents[0].Marker is QMarker);
            Assert.IsTrue(output.Contents[0][0].Marker is VMarker);
            Assert.IsTrue(output.Contents[0][0][1].Marker is QMarker);
            Assert.IsTrue(output.Contents[1].Marker is VMarker);

            string secondVerseText = "\\v 1 This is verse one \\q another poetry \\v 2 second verse";

            output = parser.ParseFromString(secondVerseText);
            Assert.AreEqual(2, output.Contents.Count);
            Assert.IsTrue(output.Contents[0].Marker is VMarker);
            Assert.IsTrue(output.Contents[0].Contents[1].Marker is QMarker);
            Assert.IsTrue(output.Contents[1].Marker is VMarker);
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
            Assert.IsTrue(output.Contents[0].Marker is VMarker);
            Assert.IsTrue(output.Contents[1].Marker is QMarker { IsPoetryBlock: true });
            Assert.IsTrue(output.Contents[1].Contents[0].Marker is VMarker);
        }

        [TestMethod]
        public void TestBadChapterHandling()
        {
            string verseText = "\\c 1 Bad text here";
            var output = parser.ParseFromString(verseText);
            var hierachy = output.Hierarchies[0];
            Assert.AreEqual(1, hierachy.Contents.Count);
            Assert.IsTrue(hierachy[0][0].Marker is TextBlock);
            Assert.AreEqual(1, ((CMarker)hierachy[0]).Number);
            Assert.AreEqual("Bad text here", ((TextBlock)hierachy[0][0]).Text);
        }

        [TestMethod]
        public void TestNoChapterNumberingHandling()
        {
            var verseText = "\\c \\v 1 Bad text here";
            var output = parser.ParseFromString(verseText);
            var hierarchy = output.Hierarchies[0];
            Assert.AreEqual(1, hierarchy.Contents.Count);
            Assert.IsTrue(hierarchy[0].Marker is CMarker);
            Assert.AreEqual(0, ((CMarker)hierarchy[0]).Number);
        }

        [TestMethod]
        public void TestNoChapterNumberingAndTextHandling()
        {
            var verseText = "\\c Text Block \\v 1 Bad text here";
            var output = parser.ParseFromString(verseText);
            var hierarchy = output.Hierarchies[0];
            Assert.AreEqual(1, hierarchy.Contents.Count);
            Assert.IsTrue(hierarchy[0].Marker is CMarker);
            Assert.AreEqual(0, ((CMarker)hierarchy[0]).Number);
            Assert.AreEqual(2, hierarchy[0].Contents.Count);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.AreEqual("Text Block", ((TextBlock)hierarchy[0][0]).Text);
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
            Assert.IsTrue(output.Hierarchies[0][0].Marker is QMarker);
        }

        [TestMethod]
        public void TestIntroParagraphs()
        {
            string text = "\\ip \\rq \\rq* \\ie";
            var output = parser.ParseFromString(text);
            var hierarchy = output.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is IPMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is RQMarker);
            Assert.IsTrue(hierarchy[0][1].Marker is RQEndMarker);
            Assert.IsTrue(hierarchy[0][3].Marker is IEMarker);
        }

        [TestMethod]
        public void TestNewlineInTextBlock()
        {
            string verseText = @"This is text 
with a newline";
            string usfm = $"\\v 1 {verseText}";
            var output = parser.ParseFromString(usfm);
            var hierarchy = output.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is VMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.AreEqual(verseText, ((TextBlock)hierarchy[0][0]).Text);
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
            var hierarchy = result.Hierarchies[0];
            Assert.AreEqual(2, hierarchy.GetChildMarkers<TextBlock>().Count);
            Assert.AreEqual(1, hierarchy.GetChildMarkers<TextBlock>(ignoredParents).Count);
            var verse = hierarchy.GetChildMarkers<VMarker>()[0];
            Assert.AreEqual(2, verse.GetChildMarkers<TextBlock>().Count);
            Assert.AreEqual(1, verse.GetChildMarkers<TextBlock>(ignoredParents).Count);
            Assert.AreEqual(0, verse.GetChildMarkers<TextBlock>(new List<Type>() { typeof(VMarker)}).Count);
        }

        [TestMethod]
        public void TestGetChildMarkers()
        {
            var result = parser.ParseFromString("\\c 1 \\v 1 Text blocks \\f \\ft Text \\f* \\v 2 Third block \\c 2 \\v 1 Fourth block");
            var markers = result.Hierarchies[0].GetChildMarkers<VMarker>();
            Assert.AreEqual(3, markers.Count);
            Assert.AreEqual("1", markers[0].As<VMarker>().VerseNumber);
            Assert.AreEqual("2", markers[1].As<VMarker>().VerseNumber);
            Assert.AreEqual("1", markers[2].As<VMarker>().VerseNumber);
        }

        [TestMethod]
        public void TestGetHierarchyToMarker()
        {
            var document = new USFMDocument();
            document.Hierarchies = [new HierarchyNode(document)];
            var chapter = new CMarker() { Number = 1 };
            var verse = new VMarker() { VerseNumber = "1" };
            var textblock = new TextBlock("Hello world");
            document.InsertMultiple([chapter, verse, textblock], [DefaultHierarchies.Default.ToFrozenDictionary()]);
            var result = document.Hierarchies[0].GetHierarchyToMarker(textblock);
            Assert.AreEqual(document, result[0]);
            Assert.AreEqual(chapter, result[1]);
            Assert.AreEqual(verse, result[2]);
            Assert.AreEqual(textblock, result[3]);

            document = parser.ParseFromString(@"\c 1\p \v 1 Before \f + \ft In footnote \f* After footnore");

            var markers = document.Hierarchies[0].GetChildMarkers<TextBlock>().ToArray();
            var baseMarker = document.Contents[0][0][0];
            var hierarchy = baseMarker.GetHierarchyToMarker(markers[0]).ToList();
            CheckTypeList([typeof(VMarker), typeof(TextBlock)], hierarchy);
            hierarchy = baseMarker.GetHierarchyToMarker(markers[1]).ToList();
            CheckTypeList([typeof(VMarker), typeof(FMarker), typeof(FTMarker), typeof(TextBlock)], hierarchy);
            hierarchy = baseMarker.GetHierarchyToMarker(markers[2]).ToList();
            CheckTypeList([typeof(VMarker), typeof(TextBlock)], hierarchy);

        }

        [TestMethod]
        public void TestGetHierarchyToMarkerWithNonExistantMarker()
        {
            var document = new USFMDocument();
            document.Hierarchies = [new HierarchyNode(document)];
            var chapter = new CMarker() { Number = 1 };
            var verse = new VMarker() { VerseNumber = "1" };
            var textblock = new TextBlock("Hello world");
            var secondBlock = new TextBlock("Hello again");
            document.InsertMultiple(new Marker[] { chapter, verse, textblock }, [DefaultHierarchies.Default.ToFrozenDictionary()]);
            var result = document.Hierarchies[0].GetHierarchyToMarker(secondBlock);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestGetHierarchyToMultipleMarkers()
        {
            var document = new USFMDocument();
            document.Hierarchies = [new HierarchyNode(document)];
            var chapter = new CMarker() { Number = 1 };
            var verse = new VMarker() { VerseNumber = "1" };
            var textblock = new TextBlock("Hello world");
            var footnote = new FMarker();
            var footnoteText = new FTMarker();
            var footnoteEndMarker = new FEndMarker();
            var textInFootnote = new TextBlock("Text in footnote");
            var secondBlock = new TextBlock("Hello again");
            var nonExistant = new VMarker();
            var result = document.Hierarchies[0].GetHierachyToMultipleMarkers([]);
            Assert.AreEqual(0, result.Count);
            result = document.Hierarchies[0].GetHierachyToMultipleMarkers([footnote]);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[footnote].Count);
            document.InsertMultiple([chapter, verse, textblock, footnote, footnoteText, textInFootnote, footnoteEndMarker, secondBlock
            ], [DefaultHierarchies.Default.ToFrozenDictionary()]);
            result = document.Hierarchies[0].GetHierachyToMultipleMarkers([
                textblock, secondBlock, nonExistant, textInFootnote
            ]);
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

        [TestMethod]
        public void VerifyNewlinesStopMarker()
        {
            var doc = parser.ParseFromString("\\c 1\n \\v 1 In the beginning ");
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is CMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is VMarker);
            Assert.AreEqual(1, ((CMarker)hierarchy[0]).Number);
        }

        [TestMethod]
        public void TestElementsAfterIgnore()
        {
            parser = new USFMParser(tagsToIgnore: ["s5"]);
            var doc = parser.ParseFromString("\\s5\n\\c 1\n \\v 1 In the beginning ");
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is CMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is VMarker);
            Assert.AreEqual(1, ((CMarker)hierarchy.Contents[0].Marker).Number);
        }

        [TestMethod]
        public void TestBackToBackMarkers()
        {
            var doc = parser.ParseFromString("\\p\\v 1 In the beginning ");
            var hierarchy = doc.Hierarchies[0];
            var pMarker = hierarchy[0].Marker;
            Assert.IsTrue(pMarker is PMarker);
            Assert.IsTrue(hierarchy[0].Contents.Count > 0);
            Assert.IsTrue(hierarchy[0][0].Marker is VMarker);
        }

        [TestMethod]
        public void TestTrailingCarriageReturnNewline()
        {

        var TestUSFMWithMissingTOC = 
                    @"\id GEN
        \c 1
        \p
        \v 1 In the beginning God created the heavens and the earth.";
            var doc = parser.ParseFromString(TestUSFMWithMissingTOC);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[1].Marker is CMarker);
            Assert.IsTrue(hierarchy[1][0].Marker is PMarker);
            Assert.IsTrue(hierarchy[1][0][0].Marker is VMarker);
            Assert.AreEqual( 1, ((CMarker)hierarchy[1].Marker).Number);
            Assert.AreEqual( 1, ((VMarker)hierarchy[1][0][0].Marker).StartingVerse);
            Assert.IsTrue(hierarchy[1][0][0][0].Marker is TextBlock);
            Assert.AreEqual("In the beginning God created the heavens and the earth.", ((TextBlock)hierarchy[1][0][0][0].Marker).Text);
        }

        [TestMethod]
        public void TestTrailingContentAfterFootnote()
        {
            var doc = parser.ParseFromString(
                @"\v 1 verse content \f + \ft footnote text \fqa Ebiasaph \fqa* in 1 Chronicles 9:19. \f*.");
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is VMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.IsTrue(hierarchy[0][1].Marker is FMarker);
            Assert.IsTrue(hierarchy[0][2].Marker is FEndMarker);
            Assert.IsTrue(hierarchy[0][3].Marker is TextBlock);
            Assert.AreEqual(".", ((TextBlock)hierarchy[0][3]).Text);
        }

        [TestMethod]
        public void TestEndMarkerAtEndOfString()
        {
            var doc = parser.ParseFromString(
                @"\bd Bold text \bd*");
            Assert.IsTrue(doc.Hierarchies[0][0].Marker is BDMarker);
            Assert.IsTrue(doc.Hierarchies[0][1].Marker is BDEndMarker);
            Assert.AreEqual(2, doc.Hierarchies[0].Contents.Count);
        }

        [TestMethod]
        public void TestNewlineEndsMarker()
        {
            // Test that unknown markers are ignored when ignoreUnknownMarkers is set to true
            parser = new USFMParser(new List<string> { "s5" }, true);
            var content = @"
\p
This next question is answered the same way in all the churches of God's people.";
            var doc = parser.ParseFromString(content);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is PMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.AreEqual("This next question is answered the same way in all the churches of God's people.", ((TextBlock)doc.Contents[0].Contents[0]).Text);
        }

        [TestMethod]
        [TestCategory("Permissive")]
        public void TestOtherSymbolEndsMarker()
        {
            var content = @"\p(this is text)";
            var doc = parser.ParseFromString(content);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is PMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.AreEqual("(this is text)", ((TextBlock)hierarchy[0][0]).Text);
        }

        [TestMethod]
        [TestCategory("Permissive")]
        public void TestPermissiveMarkerEndingWithNumber()
        {
            var content = @"\q1This is text";
            var doc = parser.ParseFromString(content);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is QMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.AreEqual("This is text", hierarchy[0][0].As<TextBlock>().Text);
        }

        [TestMethod]
        public void TestIdeographicSpaceRetentionInVerses()
        {
            var ideographicSpace = '\u3000'; // Ideographic space character
            var content = $"\\v 1 This is text{ideographicSpace}with an ideographic space.";
            var doc = parser.ParseFromString(content);
            var hierachy = doc.Hierarchies[0];
            Assert.IsTrue(hierachy[0].Marker is VMarker);
            Assert.IsTrue(hierachy[0][0].Marker is TextBlock);
            Assert.AreEqual($"This is text{ideographicSpace}with an ideographic space.", ((TextBlock)hierachy[0][0]).Text);

            content = @$"\v 1 {ideographicSpace}This is more text";
            doc = parser.ParseFromString(content);
            hierachy = doc.Hierarchies[0];
            Assert.IsTrue(hierachy[0].Marker is VMarker);
            Assert.IsTrue(hierachy[0][0].Marker is TextBlock);
            Assert.AreEqual($"{ideographicSpace}This is more text", ((TextBlock)hierachy[0][0]).Text);
        }

        [TestMethod]
        public void TestBackslashEscapedInText()
        {
            var content = @"\v 1 This is text with a backslash \\ in it.";
            var doc = parser.ParseFromString(content);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is VMarker);
            Assert.IsTrue(hierarchy[0][0].Marker is TextBlock);
            Assert.AreEqual("This is text with a backslash ", hierarchy[0][0].As<TextBlock>().Text);
            Assert.AreEqual("\\", hierarchy[0][1].As<TextBlock>().Text);
            Assert.AreEqual(" in it.", hierarchy[0][2].As<TextBlock>().Text);
            content = @"\v 1 This is text that is n\\a";
            doc = parser.ParseFromString(content);
            hierarchy = doc.Hierarchies[0];
            Assert.AreEqual("This is text that is n", hierarchy[0][0].As<TextBlock>().Text);
            Assert.AreEqual("\\", hierarchy[0][1].As<TextBlock>().Text);
            Assert.AreEqual("a", hierarchy[0][2].As<TextBlock>().Text);
        }

        [TestMethod]
        public void TestChapterLabelInChapter()
        {
            var content = @"\c 1\cl Chapter Label Text \v 1 In the beginning God created the heavens and the earth.";
            var doc = parser.ParseFromString(content);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is CMarker);
            var chapter = hierarchy[0].As<CMarker>();
            Assert.AreEqual("Chapter Label Text", chapter.CustomChapterLabel);
        }

        [TestMethod]
        public void TestPublishedChapterNumber()
        {
            var content = @"\c 1 \cp T \v 1 In the beginning God created the heavens and the earth.";
            var doc = parser.ParseFromString(content);
            var hierarchy = doc.Hierarchies[0];
            Assert.IsTrue(hierarchy[0].Marker is CMarker);
            var chapter = hierarchy[0].As<CMarker>();
            Assert.AreEqual("T", chapter.PublishedChapterMarker);
        }

        [TestMethod]
        public void TestFindingMarkerInAnotherHierarchy()
        {
            var content = @"\p \c 1 \p \v 1 In the beginning God created the heavens and the earth.";
            var doc = parser.ParseFromString(content);
            var defaultHierarchy = doc.Hierarchies[0];
            var presentationHierarchy = doc.Hierarchies[1];
            var markerPathInPresentation = presentationHierarchy.GetNodesToMarker(defaultHierarchy[1][0][0]);
            Assert.AreEqual(typeof(USFMDocument), markerPathInPresentation[0].MarkerType);
            Assert.AreEqual(typeof(PMarker), markerPathInPresentation[1].MarkerType);
            Assert.AreEqual(typeof(VMarker), markerPathInPresentation[2].MarkerType);
        }

        [TestMethod]
        public void TestAllHierarchiesPopulatedFromEmptyDocument()
        {
            // Regression test for bug in USFMDocument.Insert() method.
            // 
            // BUG: When inserting the first marker into an empty document, if the code used
            // "return" instead of "continue" (line 40), only the first hierarchy would be
            // populated and hierarchies 1 and 2 would remain empty.
            //
            // This test verifies that when inserting into an empty document, ALL hierarchies
            // (Default, Presentation, Structure) are populated, not just the first one.
            //
            // Without the fix, this test would fail with:
            // - Hierarchy 0 would have 1 item (VMarker)
            // - Hierarchy 1 would have 0 items (empty - BUG!)
            // - Hierarchy 2 would have 0 items (empty - BUG!)
            var content = @"\v 1 In the beginning God created the heavens and the earth.";
            var doc = parser.ParseFromString(content);
            
            // Verify all three hierarchies exist
            Assert.AreEqual(3, doc.Hierarchies.Count, "Should have 3 hierarchies (Default, Presentation, Structure)");
            
            // Verify all hierarchies have content (not empty)
            for (int i = 0; i < doc.Hierarchies.Count; i++)
            {
                Assert.IsTrue(doc.Hierarchies[i].Contents.Count > 0, 
                    $"Hierarchy {i} should not be empty");
            }
            
            // Verify the verse marker exists in all hierarchies
            Assert.IsInstanceOfType(doc.Hierarchies[0][0].Marker, typeof(VMarker), 
                "Default hierarchy should contain VMarker");
            Assert.IsInstanceOfType(doc.Hierarchies[1][0].Marker, typeof(VMarker), 
                "Presentation hierarchy should contain VMarker");
            Assert.IsInstanceOfType(doc.Hierarchies[2][0].Marker, typeof(VMarker), 
                "Structure hierarchy should contain VMarker");
            
            // Verify the verse number is correct in all hierarchies
            var vMarkerDefault = doc.Hierarchies[0][0].As<VMarker>();
            var vMarkerPresentation = doc.Hierarchies[1][0].As<VMarker>();
            var vMarkerStructure = doc.Hierarchies[2][0].As<VMarker>();
            
            Assert.AreEqual(1, vMarkerDefault.StartingVerse);
            Assert.AreEqual(1, vMarkerPresentation.StartingVerse);
            Assert.AreEqual(1, vMarkerStructure.StartingVerse);
        }
    }
}
