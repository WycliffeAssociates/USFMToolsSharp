using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USFMToolsSharp.Models;

namespace USFMToolsSharpTest
{

    [TestClass]
    public class HTMLRenderTest
    {
        private USFMToolsSharp.USFMParser parser;
        private USFMToolsSharp.HtmlRenderer render;
        private HTMLConfig configHTML;

        [TestInitialize]
        public void SetUpTestCase()
        {
            configHTML = new HTMLConfig(new List<string>(), partialHTML: true);

            parser = new USFMToolsSharp.USFMParser();
            render = new USFMToolsSharp.HtmlRenderer(configHTML);
            
        }
        [TestMethod]
        public void TestSectionRender()
        {
            // Section Headings
            Assert.AreEqual("<div class=\"sectionhead-1\">Silsilah Yesus Kristus</div><div class=\"section-reference\">(Luk. 3:23 - 38)</div>", WrapTest("\\s Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)"));
            Assert.AreEqual("<div class=\"sectionhead-3\">Silsilah Yesus Kristus</div><div class=\"section-reference\">(Luk. 3:23 - 38)</div>", WrapTest("\\s3 Silsilah Yesus Kristus \\r (Luk. 3:23 - 38)"));

            // Major Section 
            Assert.AreEqual("<div class=\"sectionhead-2\">jilid 1</div><div class=\"major-section-reference\">(Mazmur 1 - 41)</div>", WrapTest("\\ms2 jilid 1 \\mr (Mazmur 1 - 41)"));
            Assert.AreEqual("<div class=\"sectionhead-3\">jilid 1</div><div class=\"major-section-reference\">(Mazmur 1 - 41)</div>", WrapTest("\\ms3 jilid 1 \\mr (Mazmur 1 - 41)"));
            Assert.AreEqual("<div class=\"sectionhead-1\">jilid 1</div><div class=\"major-section-reference\">(Mazmur 1 - 41)</div>", WrapTest("\\ms jilid 1 \\mr (Mazmur 1 - 41)"));

            // References
            Assert.AreEqual("<div class=\"sectionhead-2\">jilid 1</div><div class=\"major-section-reference\">(Mazmur)</div>", WrapTest("\\ms2 jilid 1 \\mr (Mazmur)"));

        }
        [TestMethod]
        public void TestMajorTitleRender()
        {
            Assert.AreEqual("<div class=\"majortitle-1\">Keluaran</div>", WrapTest("\\mt1 Keluaran"));
            Assert.AreEqual("<div class=\"majortitle-3\">Keluaran</div>", WrapTest("\\mt3 Keluaran"));
            Assert.AreEqual("<div class=\"majortitle-1\">Keluaran</div>", WrapTest("\\mt Keluaran"));
            Assert.AreEqual("<div class=\"majortitle-2\">Keluaran</div>", WrapTest("\\mt2 Keluaran"));
        }
        [TestMethod]
        public void TestHeaderRender()
        {
            Assert.AreEqual("<div class=\"header\">Genesis</div>", WrapTest("\\h Genesis"));
            Assert.AreEqual("<div class=\"header\">1 John</div>", WrapTest("\\h 1 John"));
            Assert.AreEqual("<div class=\"header\"></div>", WrapTest("\\h"));
            Assert.AreEqual("<div class=\"header\"></div>", WrapTest("\\h      "));
            
        }
        [TestMethod]
        public void TestChapterRender()
        {

            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span></div>", WrapTest("\\c 1"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1000</span></div>", WrapTest("\\c 1000"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">-1</span></div>", WrapTest("\\c -1"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">0</span></div>", WrapTest("\\c 0"));

            // Chapter Labels
            Assert.AreEqual("<div class=\"chapter\"><div class=\"chapterlabel\">Chapter One</div><br/></div>", WrapTest("\\c 1 \\cl Chapter One"));
            Assert.AreEqual("<div class=\"chapter\"><div class=\"chapterlabel\">Chapter One</div><br/></div><div class=\"chapter\"><div class=\"chapterlabel\">Chapter Two</div><br/></div>", WrapTest("\\c 1 \\cl Chapter One \\c 2 \\cl Chapter Two"));
            Assert.AreEqual("<div class=\"chapter\"><div class=\"chapterlabel\">Chapter 1</div><br/></div><div class=\"chapter\"><div class=\"chapterlabel\">Chapter 2</div><br/></div>", WrapTest("\\cl Chapter \\c 1 \\c 2"));
            Assert.AreEqual("<div class=\"chapter\"><div class=\"chapterlabel\">Chapter 1</div><br/></div><div class=\"chapter\"><div class=\"chapterlabel\">Chapter 2</div><br/></div><div class=\"chapter\"><div class=\"chapterlabel\">Psalm 1</div><br/></div><div class=\"chapter\"><div class=\"chapterlabel\">Psalm 2</div><br/></div>", WrapTest("\\id Genesis \\cl Chapter \\c 1 \\c 2 \\id Psalms \\cl Psalm \\c 1 \\c 2"));


        }

        [TestMethod]
        public void TestVerseRender()
        {

            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span>", WrapTest("\\v 200 Genesis"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">0</span>fff</span>", WrapTest("\\v 0 fff"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>asdfasdf</span></div>", WrapTest("\\c 1  \\v 1 asdfasdf"));

            // References - Quoted book title - Parallel passage reference
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">14</span>Itulah sebabnya kata-kata ini ditulis dalam<span class=\"quoted-book\">Kitab Peperangan TUHAN,</span></span>", WrapTest("\\v 14 Itulah sebabnya kata-kata ini ditulis dalam \\bk Kitab Peperangan TUHAN,\\bk*"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">5</span>For God never said to any of his angels,<div class=\"poetry-1\">\"You are my Son;</div><div class=\"poetry-2\">today I have become your Father.\"</div><div class=\"reference\">Psa 2.7</div></span>", WrapTest("\\v 5 For God never said to any of his angels,\\q1 \"You are my Son;\\q2 today I have become your Father.\"\\rq Psa 2.7\\rq* "));

            // Closing - Selah
            Assert.AreEqual("<div class=\"closing\">[[ayt.co/Mat]]</div>", WrapTest("\\cls [[ayt.co/Mat]]"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">3</span>Allah datang dari negeri Teman<div class=\"poetry-2\">dan Yang Mahakudus datang dari Gunung Paran.<div class=\"selah-text\">Sela</div></div></span>", WrapTest("\\v 3 Allah datang dari negeri Teman \\q2 dan Yang Mahakudus datang dari Gunung Paran. \\qs Sela \\qs* "));
            Assert.AreEqual("<div class=\"poetry-2\">dan sampai batu yang penghabisan.<div class=\"selah-text\">Sela</div></div>", WrapTest("\\q2 dan sampai batu yang penghabisan. \\qs Sela \\qs*"));

            // Transliterated
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\">1</span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\">1</span><b> 10:15 </b> DUNIA ORANG MATI: Dalam bahasa Yunani adalah<span class=\"transliterated\">Hades</span>, tempat orang setelah meninggal.</div>", WrapTest("\\c 1 \\v 1 \\f + \\fr 10:15 \\fk dunia orang mati \\ft Dalam bahasa Yunani adalah \\tl Hades\\tl* \\ft , tempat orang setelah meninggal.\\f*"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\"></span><span class=\"transliterated\">TEKEL</span>:</span>", WrapTest("\\v 27 \\tl TEKEL\\tl* :"));
        }
        [TestMethod]
        public void TestTableRender()
        {
            // Table Rows - Cells
            Assert.AreEqual("<div><table class=\"table-block\"><tr><td class=\"table-cell\">dari suku Ruben</td><td class=\"table-cell-right\">12.000</td></tr></table></div>", WrapTest("\\tr \\tc1 dari suku Ruben \\tcr2 12.000"));

            // Embedded Verse
            Assert.AreEqual("<td class=\"table-cell\"><span class=\"verse\"><span class=\"versemarker\">6</span>dari suku Asyer</span></td>", WrapTest("\\tc1 \\v 6 dari suku Asyer"));

            // Table Headers
            Assert.AreEqual("<div><table class=\"table-block\"><tr><td class=\"table-head\">dari suku Ruben</td><td class=\"table-head-right\">12.000</td></tr></table></div>", WrapTest("\\tr \\th1 dari suku Ruben \\thr2 12.000"));

            
        }
        [TestMethod]
        public void TestListRender()
        {
            // List Items
            Assert.AreEqual("<div class=\"list-1\">Peres ayah Hezron.</div>", WrapTest("\\li Peres ayah Hezron."));
            // Verse within List
            Assert.AreEqual("<div class=\"list-1\">Peres ayah Hezron.</div><div class=\"list-1\"><span class=\"verse\"><span class=\"versemarker\">19</span>Hezron ayah Ram.</span></div>", WrapTest("\\li Peres ayah Hezron. \\li \\v 19 Hezron ayah Ram."));
        }
        [TestMethod]
        public void TestFootnoteRender()
        {
            // Footnote Caller - Text - Alternate Translation
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">26</span>This is a footnote<span class=\"caller\">1</span></span>", WrapTest("\\v 26 This is a footnote \\f + \\f*"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">26</span>God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\"<span class=\"caller\">2</span></span>", WrapTest("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">1</span>Sam Paul!<span class=\"caller\">3</span></span>", WrapTest("\\v 1 Sam Paul! \\f + \\ft Sample Simple Footnote. \\f*"));

            //Footnote Keyword - Reference - Verse Marker
            // Footnote Caller
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\">4</span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\">1</span></div><div class=\"footnotes\"><span class=\"caller\">2</span>Some ancient copies have:<span class=\"footnote-alternate-translation\">... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth</span>.</div><div class=\"footnotes\"><span class=\"caller\">3</span>Sample Simple Footnote.</div><div class=\"footnotes\"><span class=\"caller\">4</span>Sample Simple Footnote.</div>", WrapTest("\\c 1 \\v 1 \\f + \\ft Sample Simple Footnote. \\f*"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\"></span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\"></span>Sample Simple Footnote.</div>", WrapTest("\\c 1 \\v 1 \\f - \\ft Sample Simple Footnote. \\f*"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\">abc</span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\">abc</span>Sample Simple Footnote.</div>", WrapTest("\\c 1 \\v 1 \\f abc \\ft Sample Simple Footnote. \\f*"));

            // Footnote Keyword - Reference
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\">1</span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\">1</span><b> 1.3 </b> TAMAR: Menantu Yehuda yang akhirnya menjadi istrinya (bc. Kej. 38:1-30).</div>", WrapTest("\\c 1 \\v 1 \\f + \\fr 1.3 \\fk Tamar \\ft Menantu Yehuda yang akhirnya menjadi istrinya (bc. Kej. 38:1-30).\\f*"));

            // Footnote Verse Marker - Paragraph
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\">1</span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\">1</span><b> 9:55 </b>Beberapa<span class=\"versemarker\">56</span>untuk menyelamatkan mereka.</div>", WrapTest("\\c 1 \\v 1 \\f + \\fr 9:55 \\ft Beberapa  \\fv 56 \\ft untuk menyelamatkan mereka.\\f*"));
            

        }
        [TestMethod]
        public void TestCrossReferenceRender()
        {
            // Cross Reference Caller
            Assert.AreEqual("<span class=\"caller\"></span>", WrapTest("\\x - \\xo 11.21 \\xq Tebes \\xt \\x*"));

            // Cross Reference Origin
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\"></span></span></div><hr/><span class=\"cross-ref\"><span class=\"caller\"></span><b> 11.21 </b><span class=\"cross-ref-quote\">Tebes</span> </span><span class=\"cross-ref\"><span class=\"caller\"></span><b> 11.21 </b><span class=\"cross-ref-quote\">Tebes</span> </span>", WrapTest("\\c 1 \\v 1 \\x - \\xo 11.21 \\xq Tebes \\xt \\x*"));

            // Cross Reference Target
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\"></span></span></div><hr/><span class=\"cross-ref\"><span class=\"caller\"></span><b> 11.21 </b><span class=\"cross-ref-quote\">Tebes</span>Mrk 1.24; Luk 2.39; Jhn 1.45. </span>", WrapTest("\\c 1 \\v 1 \\x - \\xo 11.21 \\xq Tebes \\xt Mrk 1.24; Luk 2.39; Jhn 1.45.\\x*"));

            // Cross Reference Quotation
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\"></span></span></div><hr/><span class=\"cross-ref\"><span class=\"caller\"></span><b> 11.21 </b><span class=\"cross-ref-quote\">Tebes</span> </span>", WrapTest("\\c 1 \\v 1 \\x - \\xo 11.21 \\xq Tebes \\xt \\x*"));
        }
        [TestMethod]
        public void TestVPRender()
        {
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">1a</span>This is not Scripture</span>", WrapTest("\\v 1 \\vp 1a \\vp* This is not Scripture"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">2b</span>This is not Scripture</span>", WrapTest("\\v 2 \\vp 2b \\vp* This is not Scripture"));
        }
        [TestMethod]
        public void TestWordEntryRender()
        {

            // Within Footnotes
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\"></span><span class=\"caller\">1</span></span></div><hr/><div class=\"footnotes\"><span class=\"caller\">1</span><b> 3:5 </b> BERHALA: Lih.<span class=\"word-entry\"> Berhala </span>di Daftar Istilah.</div>", WrapTest("\\c 1 \\v 1\\f + \\fr 3:5 \\fk berhala \\ft Lih. \\w Berhala \\w* di Daftar Istilah.\\f*"));

            // Word Entry Attributes
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\"></span><span class=\"word-entry\"> Berhala </span>di Daftar Istilah</span>", WrapTest("\\v 1 \\w Berhala|Berhala \\w* di Daftar Istilah"));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\"></span><span class=\"word-entry\"> gracious </span>di Daftar Istilah.</span>", WrapTest("\\v 1 \\w gracious|lemma=\"grace\" \\w* di Daftar Istilah."));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\"></span><span class=\"word-entry\"> gracious </span>di Daftar Istilah.</span>", WrapTest("\\v 1 \\w gracious|lemma=\"grace\" strong=\"G5485\" \\w* di Daftar Istilah."));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\"></span><span class=\"word-entry\"> gracious </span>di Daftar Istilah.</span>", WrapTest("\\v 1 \\w gracious|strong=\"H1234,G5485\" \\w* di Daftar Istilah."));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\"></span><span class=\"word-entry\"> gracious </span>di Daftar Istilah.</span>", WrapTest("\\v 1 \\w gracious|lemma=\"grace\" srcloc=\"gnt5:51.1.2.1\" \\w* di Daftar Istilah."));

        }
        [TestMethod]
        public void TestCharacterStylingRender()
        {
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<span class=\"emphasis\">Emphasis</span></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\em Emphasis \\em* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<b>Boldness</b></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\bd Boldness \\bd* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<span class=\"bold-italic\">Boldness and Italics</span></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\bdit Boldness and Italics \\bdit* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<i>Italics</i></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\it Italics \\it* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<span class=\"superscript-text\">Superscript</span></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\sup Superscript \\sup* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<span class=\"deity-name\">Name of Diety</span></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\nd Name of Diety \\nd* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<span class=\"small-caps\">Small Caps</span></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\sc Small Caps \\sc* "));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">21</span>Penduduk kota yang satu akan pergi<span class=\"normal-text\">Normal</span></span>", WrapTest("\\v 21 Penduduk kota yang satu akan pergi \\no Normal \\no* "));


        }
        [TestMethod]
        public void TestStyleRender()
        {
            render.ConfigurationHTML.divClasses.Add("two-columns");
            Assert.AreEqual("<div class=\"two-columns\"><span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span></div>", WrapTest("\\v 200 Genesis"));
            render.ConfigurationHTML.divClasses.Add("justified");
            Assert.AreEqual("<div class=\"two-columns\"><div class=\"justified\"><span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span></div></div>", WrapTest("\\v 200 Genesis"));

        }
        [TestMethod]
        public void TestChapterBreak()
        {
            render.ConfigurationHTML.separateChapters = true;
            Assert.AreEqual("<div class=\"majortitle-1\">Genesis</div><div class=\"chapter\"><span class=\"chaptermarker\">1</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div><div class=\"chapter\"><span class=\"chaptermarker\">2</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div><div class=\"chapter\"><span class=\"chaptermarker\">3</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div>", WrapTest("\\mt Genesis \\c 1 \\c 2 \\c 3"));
            Assert.AreEqual("<div class=\"majortitle-1\">Genesis</div><div class=\"chapter\"><span class=\"chaptermarker\">1</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div><div class=\"chapter\"><span class=\"chaptermarker\">2</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div><div class=\"majortitle-1\">Exodus</div><div class=\"chapter\"><span class=\"chaptermarker\">1</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div>", WrapTest("\\mt Genesis \\c 1 \\c 2 \\mt Exodus \\c 1"));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>First</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div><div class=\"chapter\"><span class=\"chaptermarker\">2</span><span class=\"verse\"><span class=\"versemarker\">1</span>Second</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div><div class=\"chapter\"><span class=\"chaptermarker\">3</span><span class=\"verse\"><span class=\"versemarker\">1</span>Third</span></div><br class=\"pagebreak\"></br><div class=\"pagebreak\"></div>", WrapTest("\\c 1 \\v 1 First \\c 2 \\v 1 Second \\c 3 \\v 1 Third"));

        }
        [TestMethod]
        public void TestUnknownMarkerRender()
        {

            Assert.AreEqual("", WrapTest("\\yy"));
            Assert.AreEqual("<div class=\"header\">1 John</div>", WrapTest("\\h 1 John \\test"));
            Assert.AreEqual("", WrapTest("\\123 sdfgsgdfg"));

        }
        public string stripWhiteSpace(string input)
        {
            return input.Replace("\r", "").Replace("\n", "");
        }
        public string WrapTest(string usfm)
        {
            return stripWhiteSpace(render.Render(parser.ParseFromString(usfm)));
        }

    }
}
