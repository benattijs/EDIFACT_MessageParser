using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDIFACTMessageParser;
using System.Collections.Generic;

namespace EDIFACTMessageParserUnitTests
{
    [TestClass]
    public class ParserTests
    {

        #region ConstructorTests
   
        [TestMethod]
        public void TestSegmentConstructorWith3Elements()
        {
            string[] element = new string[] { "LOC", "08", "AAABBBCCC"};

            Segment seg = new Segment(element);

            AssertIndividualElements(element, seg);

        }

        #endregion

        [TestMethod]
        public void TestSegmentConstructorWith5Elements()
        {
            string[] element = new string[] { "LOC", "45", "ABCDE", "865454", "SOL'"};

            Segment seg = new Segment(element);

            AssertIndividualElements(element, seg);

        }

        #region ParserTests
        [TestMethod]
        public void TestMessageWithSingleLine()
        {
            string message = "LOC+25+ABC34'";
            List<Segment> response = Parser.ParseAllElements(message);


            string[] element = new String[] { "LOC", "25", "ABC34'" };
            AssertIndividualElements(element, response[0]);

        }


        [TestMethod]
        public void TestMessageWithUnsupportedTag()
        {
            string message = "NNN+25+ABC34'";
            List<Segment> response = Parser.ParseAllElements(message);

            //Should return a empty response
            Assert.AreEqual(0, response.Count);
            
        }

        [TestMethod]
        public void TestMessageWithMultipleLines()
        {
            string message = @"LOC+01+ABC34'
                             LOC+02+DEEFFGH'
                             LOC+03+XYZ8989'
                             ";
            List<Segment> response = Parser.ParseAllElements(message);

            Assert.AreEqual(3, response.Count);

            string[] element = new String[] { "LOC", "01", "ABC34'" };
            AssertIndividualElements(element, response[0]);

            element = new String[] { "LOC", "02", "DEEFFGH'" };
            AssertIndividualElements(element, response[1]);

            element = new String[] { "LOC", "03", "XYZ8989'" };
            AssertIndividualElements(element, response[2]);

        }
        [TestMethod]
        public void TestMessageWithGivenMessage()//This test might break in case any of the other SegmentTags in this message is also parsed.
        {
            string message = @"UNA:+.? '
                                UNB+UNOC:3+2021000969+4441963198+180525:1225+3VAL2MJV6EH9IX+KMSV7HMD+CUSDECU-IE++1++1'
                                UNH+EDIFACT+CUSDEC:D:96B:UN:145050'
                                BGM+ZEM:::EX+09SEE7JPUV5HC06IC6+Z'
                                LOC+17+IT044100'
                                LOC+18+SOL'
                                LOC+35+SE'
                                LOC+36+TZ'
                                LOC+116+SE003033'
                                DTM+9:20090527:102'
                                DTM+268:20090626:102'
                                DTM+182:20090527:102'
                                ";

            List<Segment> response = Parser.ParseAllElements(message);

            //Test the total amount of elements returned
            Assert.AreEqual(5, response.Count);

            string[] element = new String[] { "LOC", "17", "IT044100'" };
            AssertIndividualElements(element, response[0]);

            element = new String[] { "LOC", "18", "SOL'" };
            AssertIndividualElements(element, response[1]);

            element = new String[] { "LOC", "35", "SE'" };
            AssertIndividualElements(element, response[2]);

            element = new String[] { "LOC", "36", "TZ'" };
            AssertIndividualElements(element, response[3]);

            element = new String[] { "LOC", "116", "SE003033'" };
            AssertIndividualElements(element, response[4]);

        }

        [TestMethod]
        public void TestMessageWithMultipleElementTypes()
        {
            //Using NNN and MMM as Segment tags to avoid breaking the test in case other more standar tags are added to the Return message in future changes.
            string message = @"LOC+01+ABC34'
                             NNN+02+DEEFFGH'
                             MMM+03+XYZ8989'
                             LOC+04+XYZDER8989'
                             ";
            List<Segment> response = Parser.ParseAllElements(message);

            Assert.AreEqual(2, response.Count);

            string[] element = new String[] { "LOC", "01", "ABC34'" };
            AssertIndividualElements(element, response[0]);

            element = new String[] { "LOC", "04", "XYZDER8989'" };
            AssertIndividualElements(element, response[1]);


        }
        #endregion

        #region Helpers
        public void AssertIndividualElements(string[] element, Segment seg)
        {
            Assert.AreEqual(element[0], seg.Tag);

            for (int i = 1; i < element.Length; i++)
            {
                Assert.AreEqual(element[i], seg.Elements[i - 1]);
            }
        }
        #endregion
    }
}
