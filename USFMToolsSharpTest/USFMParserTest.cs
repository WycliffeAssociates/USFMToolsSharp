using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [TestMethod]
        public void TestHeaderParse()
        {

            Assert.AreEqual("Genesis",((HMarker)parser.ParseFromString("\\h Genesis").Contents[0]).HeaderText);
            Assert.AreEqual("", ((HMarker)parser.ParseFromString("\\h").Contents[0]).HeaderText);
            Assert.AreEqual("1 John", ((HMarker)parser.ParseFromString("\\h 1 John").Contents[0]).HeaderText);
            Assert.AreEqual("", ((HMarker)parser.ParseFromString("\\h   ").Contents[0]).HeaderText);

        }
        [TestMethod]
        public void TestChapterParse()
        {

            Assert.AreEqual(1, ((CMarker)parser.ParseFromString("\\c 1").Contents[0]).Number);
            Assert.AreEqual(1000, ((CMarker)parser.ParseFromString("\\c 1000").Contents[0]).Number);
            Assert.AreEqual(0, ((CMarker)parser.ParseFromString("\\c 0").Contents[0]).Number);
            Assert.AreEqual(-1, ((CMarker)parser.ParseFromString("\\c -1").Contents[0]).Number);

        }

        [TestMethod]
        public void TestVerseParse()
        {
            Assert.AreEqual("9", ((VMarker)parser.ParseFromString("\\v 9 Yahweh God called to the man and said to him, \"Where are you?\"").Contents[0]).VerseNumber);
            Assert.AreEqual("26", ((VMarker)(parser.ParseFromString("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*").Contents[0])).VerseNumber);
            Assert.AreEqual("0", ((VMarker)parser.ParseFromString("\\v 0 Not in the Bible").Contents[0]).VerseNumber);
            Assert.AreEqual("-1", ((VMarker)parser.ParseFromString("\\v -1 Not in the Bible").Contents[0]).VerseNumber);

        }

        [TestMethod]
        public void TestFootnoteParse()
        {
            Assert.AreEqual("Sample Simple Footnote.", ((TextBlock)parser.ParseFromString("\\f + \\ft Sample Simple Footnote. \\f*").Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth", ((TextBlock)parser.ParseFromString("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*").Contents[0].Contents[1].Contents[0].Contents[1].Contents[0]).Text);
            
        }

        [TestMethod]
        public void TestVerseCharacterParse()
        {
            Assert.AreEqual("1a", ((VPMarker)parser.ParseFromString("\\v 1 \\vp 1a \\vp* This is not Scripture").Contents[0].Contents[0]).VerseCharacter);
            Assert.AreEqual("2b", ((VPMarker)parser.ParseFromString("\\v 2 \\vp 2b \\vp* This is not Scripture").Contents[0].Contents[0]).VerseCharacter);
            Assert.AreEqual("asdf", ((VPMarker)parser.ParseFromString("\\v 1 \\vp asdf \\vp* This is not Scripture").Contents[0].Contents[0]).VerseCharacter);
            
        }

        [TestMethod]
        public void TestUnknownMarkerParse()
        {
            Assert.AreEqual("what is yy?", ((UnknownMarker)parser.ParseFromString("\\yy what is yy?").Contents[0]).ParsedValue);
            Assert.AreEqual("yy", ((UnknownMarker)parser.ParseFromString("\\yy what is yy?").Contents[0]).ParsedIdentifier);
            Assert.AreEqual("what is z?", ((UnknownMarker)parser.ParseFromString("\\z what is z?").Contents[0]).ParsedValue);
            Assert.AreEqual("z", ((UnknownMarker)parser.ParseFromString("\\z what is z?").Contents[0]).ParsedIdentifier);
            Assert.AreEqual("what is 1?", ((UnknownMarker)parser.ParseFromString("\\1  what is 1?").Contents[0]).ParsedValue);
            Assert.AreEqual("1", ((UnknownMarker)parser.ParseFromString("\\1  what is 1?").Contents[0]).ParsedIdentifier);
            
        }
        static void Main(string[] args)
        {
            // Display the number of command line arguments:
            USFMParserTest test = new USFMParserTest();
            test.TestHeaderParse();
        }
    }
}
