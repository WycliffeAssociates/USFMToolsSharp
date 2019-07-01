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
            parser = new USFMToolsSharp.USFMParser();
            render = new USFMToolsSharp.HtmlRenderer();
            configHTML = new HTMLConfig(new List<string>(), partialHTML = true);
        }
        [TestMethod]
        public void TestHeaderRender()
        {
            Assert.AreEqual("<div class=\"header\">Genesis</div>", render.Render(parser.ParseFromString("\\h Genesis")));

            // RenderMarker(Marker input)
            TestCase[] tests =
            {
                new TestCase("<div class=\"header\">Genesis</div>",render.Render(parser.ParseFromString("\\h Genesis"))),
                new TestCase("<div class=\"header\">1 John</div>",render.Render(parser.ParseFromString("\\h 1 John"))),
                new TestCase("<div class=\"header\"></div>",render.Render(parser.ParseFromString("\\h"))),
                new TestCase("<div class=\"header\"></div>",render.Render(parser.ParseFromString("\\h      ")))
            };
            List<TestCase> TestUSFM = new List<TestCase>(tests);

            foreach (TestCase test in TestUSFM)
            {
                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }
        [TestMethod]
        public void TestChapterRender()
        {
            TestCase[] tests =
            {
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">1</span></div>",render.Render(parser.ParseFromString("\\c 1"))),
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">1000</span></div>",render.Render(parser.ParseFromString("\\c 1000"))),
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">-1</span></div>",render.Render(parser.ParseFromString("\\c -1"))),
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">0</span></div>",render.Render(parser.ParseFromString("\\c 0")))
            };
            List<TestCase> TestUSFM = new List<TestCase>(tests);

            foreach (TestCase test in TestUSFM)
            {
                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }

        [TestMethod]
        public void TestVerseRender()
        {
            TestCase[] tests =
            {
                new TestCase("<span class=\"verse\"><span class=\"versemarker\">200</span>Genesis</span>",render.Render(parser.ParseFromString("\\v 200 Genesis"))),
                //new TestCase("<span class=\"verse\"><span class=\"versemarker\">0</span></span>",render.Render(parser.ParseFromString("\\v 0"))),
                new TestCase("<span class=\"verse\"><span class=\"versemarker\">0</span>fff</span>",render.Render(parser.ParseFromString("\\v 0 fff"))),
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>asdfasdf</span></div>",render.Render(parser.ParseFromString("\\c 1  \\v 1 asdfasdf")))
            };
            List<TestCase> TestUSFM = new List<TestCase>(tests);

            foreach (TestCase test in TestUSFM)
            {
                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }
        [TestMethod]
        public void TestFootnoteRender()
        {
            TestCase[] tests =
            {
                new TestCase("<span class=\"versemarker\">26</span>This is a footnote<span class=\"footnotecaller\">1</span>",render.Render(parser.ParseFromString("\\v 26 This is a footnote \\f + \\f*"))),
                //new TestCase("<span class=\"verse\"><span class=\"versemarker\">0</span></span>",render.Render(parser.ParseFromString("\\v 0"))),
                new TestCase("<span class=\"verse\"><span class=\"versemarker\">0</span>fff</span>",render.Render(parser.ParseFromString("\\v 0 fff"))),
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>asdfasdf</span></div>",render.Render(parser.ParseFromString("\\c 1  \\v 1 asdfasdf")))
            };
            List<TestCase> TestUSFM = new List<TestCase>(tests);

            foreach (TestCase test in TestUSFM)
            {
                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }
        public void TestVPRender()
        {
            TestCase[] tests =
            {
                new TestCase("<span class=\"versemarker\">26</span>This is a footnote<span class=\"footnotecaller\">1</span>",render.Render(parser.ParseFromString("\\v 26 This is a footnote \\f + \\f*"))),
                //new TestCase("<span class=\"verse\"><span class=\"versemarker\">0</span></span>",render.Render(parser.ParseFromString("\\v 0"))),
                new TestCase("<span class=\"verse\"><span class=\"versemarker\">0</span>fff</span>",render.Render(parser.ParseFromString("\\v 0 fff"))),
                new TestCase("<div class=\"chapter\"><span class=\"chaptermarker\">1</span><span class=\"verse\"><span class=\"versemarker\">1</span>asdfasdf</span></div>",render.Render(parser.ParseFromString("\\c 1  \\v 1 asdfasdf")))
            };
            List<TestCase> TestUSFM = new List<TestCase>(tests);

            foreach (TestCase test in TestUSFM)
            {
                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }
        [TestMethod]
        public void TestStyleRender()
        {
            TestStyleCase[] tests =
            {
                new TestStyleCase("<div class=\"two-columns\">","\\v 26 This is a footnote \\f + \\f*",new USFMToolsSharp.Models.HTMLConfig(new List<string>{ "two-columns"})),
                //new TestStyleCase("<span class=\"verse\"><span class=\"versemarker\">0</span></span>","\\v 0"))),
                new TestStyleCase("<div class=\"justified\"><div class=\"single-space\">","\\v 0 fff",new USFMToolsSharp.Models.HTMLConfig(new List<string>{ "justified","single-space"})),
                new TestStyleCase("<br class=\"pagebreak\"></br>","\\c 1  \\v 1 asdfasdf \\c 2",new USFMToolsSharp.Models.HTMLConfig(new List<string>(),true))
            };
            List<TestStyleCase> TestUSFM = new List<TestStyleCase>(tests);

            foreach (TestStyleCase test in TestUSFM)
            {

                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }
        [TestMethod]
        public void TestUnknownMarkerRender()
        {
            TestCase[] tests =
            {
                new TestCase("",render.Render(parser.ParseFromString("\\yy"))),
                new TestCase("<div class=\"header\">1 John</div>",render.Render(parser.ParseFromString("\\h 1 John \\test"))),
                new TestCase("",render.Render(parser.ParseFromString("\\123 sdfgsgdfg")))
            };
            List<TestCase> TestUSFM = new List<TestCase>(tests);

            foreach (TestCase test in TestUSFM)
            {
                Assert.IsTrue(test.actual.Contains(test.expected));
            }
        }
        public string strip_WhiteSpace(string input)
        {
            return input.Replace("\r", "").Replace("\n", "");
        }

    }
}
