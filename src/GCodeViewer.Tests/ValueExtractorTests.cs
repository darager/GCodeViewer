using GCodeViewer.WPF.Components.PointExtraction;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace GCodeViewer.Tests
{
    [TestFixture]
    public class ValueExtractorTests
    {
        // When working with gcode the printer does not allow for an accuracy of over 2 decimal points
        [Test]
        [TestCase(" X2.0 ", 2.0f)]
        [TestCase(" X10 ", 10f)]
        [TestCase(" Z9.1 ", 9.1f)]
        [TestCase(" X530.3 ", 530.3f)]
        public void When_MatchContainsValue_ReturnsValue(string matchValue, double expectedValue)
        {
            var extractor = new ValueExtractor();

            double actualValue = extractor.ExtractValue(matchValue);

            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(AreSameValue(expectedValue, actualValue));
        }

        public bool AreSameValue(double d1, double d2, int accuracy = 2)
        {
            bool result = Math.Abs(d1 - d2) < Math.Pow(10, -accuracy);
            return result;
        }
        [Test]
        [TestCase(530.32, 530.31989238)]
        [TestCase(-32.20, -32.1992389)]
        public void When_DoublesAreCloseEnough_ReturnsTrue(double d1, double d2)
        {
            bool result = AreSameValue(d1, d2);

            Assert.IsTrue(result);
        }
        [Test]
        [TestCase(530.32, 30.38)]
        [TestCase(32, -32)]
        public void When_DoublesAreNotEqual_ReturnsFalse(double d1, double d2)
        {
            bool result = AreSameValue(d1, d2);

            Assert.IsFalse(result);
        }
    }
}
