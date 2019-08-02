using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestSectionParse()
        {
            // Section Headings
            Assert.AreEqual("Silsilah Yesus Kristus", ((SMarker)parser.ParseFromString("\\s Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Text);
            Assert.AreEqual("Kumpulkanlah Harta di Surga", ((SMarker)parser.ParseFromString("\\s3 Kumpulkanlah Harta di Surga \\r (Luk. 12:33 - 34; 11:34 - 36; 16:13)").Contents[0]).Text);
            Assert.AreEqual(1, ((SMarker)parser.ParseFromString("\\s Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Weight);
            Assert.AreEqual(3, ((SMarker)parser.ParseFromString("\\s3 Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)").Contents[0]).Weight);

            // Major Section 
            Assert.AreEqual("jilid 1", ((MSMarker)parser.ParseFromString("\\ms1 jilid 1 \\mr (Mazmur 1 - 41)").Contents[0]).Heading);
            Assert.AreEqual("jilid 1", ((MSMarker)parser.ParseFromString("\\ms2 jilid 1 \\mr (Mazmur 1 - 41)").Contents[0]).Heading);
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
            Assert.AreEqual(2, ((TCRMarker)parser.ParseFromString("\\tr \\tc1 dari suku Ruben \\tcr2 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);

            // Embedded Verse
            Assert.AreEqual("6", ((VMarker)parser.ParseFromString("\\tc1 \\v 6 dari suku Asyer").Contents[0].Contents[0]).VerseNumber);

            // Table Headers
            Assert.AreEqual("dari suku Ruben", ((TextBlock)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[0].Contents[0]).Text);
            Assert.AreEqual("12.000", ((TextBlock)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[1].Contents[0]).Text);
            Assert.AreEqual(1, ((THMarker)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[0]).ColumnPosition);
            Assert.AreEqual(2, ((THRMarker)parser.ParseFromString("\\tr \\th1 dari suku Ruben \\thr2 12.000").Contents[0].Contents[0].Contents[1]).ColumnPosition);


        }
        [TestMethod]
        public void TestListParse()
        {
            var obj = parser.ParseFromString("\\li Peres ayah Hezron.");
            // List Items
            Assert.AreEqual("Peres ayah Hezron.", ((TextBlock)parser.ParseFromString("\\li Peres ayah Hezron.").Contents[0].Contents[0]).Text);
            // Verse within List
            Assert.AreEqual("19", ((VMarker)parser.ParseFromString("\\li Peres ayah Hezron. \\li \\v 19 Hezron ayah Ram.").Contents[1].Contents[0]).VerseNumber);
        }
        [TestMethod]
        public void TestFootnoteParse()
        {
            // Footnote Text Marker
            Assert.AreEqual("Sample Simple Footnote.", ((TextBlock)parser.ParseFromString("\\f + \\ft Sample Simple Footnote. \\f*").Contents[0].Contents[0].Contents[0]).Text);

            // Footnote Caller
            Assert.AreEqual("+", ((FMarker)parser.ParseFromString("\\f + \\ft Sample Simple Footnote. \\f*").Contents[0]).FootNoteCaller);
            Assert.AreEqual("-", ((FMarker)parser.ParseFromString("\\f - \\ft Sample Simple Footnote. \\f*").Contents[0]).FootNoteCaller);
            Assert.AreEqual("abc", ((FMarker)parser.ParseFromString("\\f abc \\ft Sample Simple Footnote. \\f*").Contents[0]).FootNoteCaller);

            // Footnote Alternate Translation Marker
            Assert.AreEqual("... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth", ((TextBlock)parser.ParseFromString("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*").Contents[0].Contents[1].Contents[0].Contents[1].Contents[0]).Text);

            // Footnote Keyword
            Assert.AreEqual("Tamar", ((FKMarker)parser.ParseFromString("\\f + \\fr 1.3 \\fk Tamar \\ft Menantu Yehuda yang akhirnya menjadi istrinya (bc. Kej. 38:1-30).\\f*").Contents[0].Contents[1]).FootNoteKeyword);

            //Footnote Reference
            Assert.AreEqual("1.3", ((FRMarker)parser.ParseFromString("\\f + \\fr 1.3 \\fk Tamar \\ft Menantu Yehuda yang akhirnya menjadi istrinya (bc. Kej. 38:1-30).\\f*").Contents[0].Contents[0]).VerseReference);

            // Footnote Verse Marker - Paragraph
            Assert.AreEqual("56", ((FVMarker)parser.ParseFromString("\\f + \\fr 9:55 \\ft Beberapa salinan Bahasa Yunani menambahkan: Dan ia berkata, Kamu tidak tahu roh apa yang memilikimu. \\fv 56 \\ft Anak Manusia tidak datang untuk menghancurkan hidup manusia, tetapi untuk menyelamatkan mereka.\\f*").Contents[0].Contents[2]).VerseCharacter);
            Assert.IsInstanceOfType(parser.ParseFromString("\\f + \\fr 17.25 \\ft Kemungkinan maksudnya adalah bebas dari kewajiban pajak seumur hidup. (bdk. NIV. NET) \\fp \\f*").Contents[0].Contents[2],typeof(FPMarker));
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
    }
}
