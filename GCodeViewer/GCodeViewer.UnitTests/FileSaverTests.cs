using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Objects;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCodeViewer.UnitTests
{
    [TestFixture]
    public class FileSaverTests
    {
        Mock<ITextViewModel> mockTextViewModel;
        Mock<IFile> mockFile;
        Mock<IFileChooser> mockFileChooser;

        string[] TestMessage = { "this", "is", "a", "test", "message" };

        [SetUp]
        public void Setup()
        {
            mockTextViewModel = new Mock<ITextViewModel>();
            mockTextViewModel.Setup(foo => foo.GetCurrentContent()).Returns(TestMessage);

            mockFile = new Mock<IFile>();
            mockFile.Setup(foo => foo.FilePath).Returns("mockFile.gcode");

            mockFileChooser = new Mock<IFileChooser>();
            mockFileChooser.Setup(foo => foo.GetFile()).Returns(mockFile.Object);
        }

        [Test]
        public void CanSaveFile()
        {
            string[] expected = TestMessage;
            string[] actual = new string[10];
            var fileSaver = new FileSaver(mockTextViewModel.Object, mockFileChooser.Object);

            fileSaver.WriteToFile();


            Assert.AreEqual(expected, actual);
        }
    }
}
