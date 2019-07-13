using GCodeViewer.WPF.Components;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace GCodeViewer.Tests
{
    [TestFixture]
    public class RegexBuilderTests
    {
        [Test]
        [TestCase('X', " X2.0 Z20.5")]
        [TestCase('E', " E102.9 ;Comment")]
        [TestCase('X', " X2.0234 ")]
        [TestCase('Y', " Y2 ")]
        [TestCase('Z', " Z2023.02 ")]
        public void When_InputContainsPattern_ReturnsTrue(char axisCharacter, string input)
        {
            var regexBuilder = new RegexBuilder();
            Regex regex = regexBuilder.GetAxisPattern(axisCharacter);

            bool result = regex.IsMatch(input);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase('X', " x2.0 ")]
        [TestCase('E', " Es102.9 ")]
        [TestCase('X', " aX2.0234 ")]
        [TestCase('Y', " Y2. .")]
        [TestCase('Z', " Z2023.02. ")]
        public void When_InputDoesNotContainPattern_ReturnsFalse(char axisCharacter, string input)
        {
            var regexBuilder = new RegexBuilder();
            Regex regex = regexBuilder.GetAxisPattern(axisCharacter);

            bool result = regex.IsMatch(input);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase('E', "")]
        [TestCase('X', "")]
        [TestCase('Z', "")]
        public void When_InputIsEmpty_ReturnsFalse(char axisCharacter, string input)
        {
            var regexBuilder = new RegexBuilder();
            Regex regex = regexBuilder.GetAxisPattern(axisCharacter);

            bool result = regex.IsMatch(input);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase('X', null)]
        [TestCase('Z', null)]
        public void When_InputIsNull_ThrowsArgumentNullException(char axisCharacter, string input)
        {
            var regexBuilder = new RegexBuilder();
            Regex regex = regexBuilder.GetAxisPattern(axisCharacter);

            Assert.Throws<ArgumentNullException>(() => regex.IsMatch(input));
        }
    }
}