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
        public void TestHeaderRender()
        {
            Assert.AreEqual("<div class=\"header\">Genesis</div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\h Genesis"))));
            Assert.AreEqual("<div class=\"header\">1 John</div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\h 1 John"))));
            Assert.AreEqual("<div class=\"header\"></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\h"))));
            Assert.AreEqual("<div class=\"header\"></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\h      "))));
            
        }
        [TestMethod]
        public void TestChapterRender()
        {

            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\c 1"))));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1000</span></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\c 1000"))));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">-1</span></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\c -1"))));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">0</span></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\c 0"))));
            
        }

        [TestMethod]
        public void TestVerseRender()
        {

            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 200 Genesis"))));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">0</span>fff</span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 0 fff"))));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>asdfasdf</span></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\c 1  \\v 1 asdfasdf"))));

        }
        [TestMethod]
        public void TestFootnoteRender()
        {

            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">26</span>This is a footnote<span class=\"footnotecaller\">1</span></span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 26 This is a footnote \\f + \\f*"))));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">26</span>God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\"<span class=\"footnotecaller\">2</span></span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 26 God said, \"Let us make man in our image, after our likeness. Let them have dominion over the fish of the sea, over the birds of the sky, over the livestock, over all the earth, and over every creeping thing that creeps on the earth.\" \\f + \\ft Some ancient copies have: \\fqa ... Over the livestock, over all the animals of the earth, and over every creeping thing that creeps on the earth \\fqa*  . \\f*"))));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">1</span>Sam Paul!<span class=\"footnotecaller\">3</span></span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 1 Sam Paul! \\f + \\ft Sample Simple Footnote. \\f*"))));

        }
        [TestMethod]
        public void TestVPRender()
        {
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">1a</span>This is not Scripture</span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 1 \\vp 1a \\vp* This is not Scripture"))));
            Assert.AreEqual("<span class=\"verse\"><span class=\"versemarker\">2b</span>This is not Scripture</span>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 2 \\vp 2b \\vp* This is not Scripture"))));
        }
        [TestMethod]
        public void TestStyleRender()
        {
            render.ConfigurationHTML.divClasses.Add("two-columns");
            Assert.AreEqual("<div class=\"two-columns\"><span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 200 Genesis"))));
            render.ConfigurationHTML.divClasses.Add("justified");
            Assert.AreEqual("<div class=\"two-columns\"><div class=\"justified\"><span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span></div></div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\v 200 Genesis"))));

        }
        [TestMethod]
        public void TestChapterBreak()
        {
            render.ConfigurationHTML.separateChapters = true;
            Assert.AreEqual("<div class=\"majortitle\">Genesis</div><div class=\"chapter\"><span class=\"chaptermarker\">1</span></div><br class=\"pagebreak\"></br><div class=\"chapter\"><span class=\"chaptermarker\">2</span></div><br class=\"pagebreak\"></br><div class=\"chapter\"><span class=\"chaptermarker\">3</span></div><br class=\"pagebreak\"></br>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\mt Genesis \\c 1 \\c 2 \\c 3"))));
            Assert.AreEqual("<div class=\"majortitle\">Genesis</div><div class=\"chapter\"><span class=\"chaptermarker\">1</span></div><br class=\"pagebreak\"></br><div class=\"chapter\"><span class=\"chaptermarker\">2</span></div><br class=\"pagebreak\"></br><div class=\"majortitle\">Exodus</div><div class=\"chapter\"><span class=\"chaptermarker\">1</span></div><br class=\"pagebreak\"></br>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\mt Genesis \\c 1 \\c 2 \\mt Exodus \\c 1"))));
            Assert.AreEqual("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>First</span></div><br class=\"pagebreak\"></br><div class=\"chapter\"><span class=\"chaptermarker\">2</span><span class=\"verse\"><span class=\"versemarker\">1</span>Second</span></div><br class=\"pagebreak\"></br><div class=\"chapter\"><span class=\"chaptermarker\">3</span><span class=\"verse\"><span class=\"versemarker\">1</span>Third</span></div><br class=\"pagebreak\"></br>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\c 1 \\v 1 First \\c 2 \\v 1 Second \\c 3 \\v 1 Third"))));

        }
        [TestMethod]
        public void TestUnknownMarkerRender()
        {

            Assert.AreEqual("", strip_WhiteSpace(render.Render(parser.ParseFromString("\\yy"))));
            Assert.AreEqual("<div class=\"header\">1 John</div>", strip_WhiteSpace(render.Render(parser.ParseFromString("\\h 1 John \\test"))));
            Assert.AreEqual("", strip_WhiteSpace(render.Render(parser.ParseFromString("\\123 sdfgsgdfg"))));

        }
        public string strip_WhiteSpace(string input)
        {
            return input.Replace("\r", "").Replace("\n", "");
        }

    }
}
