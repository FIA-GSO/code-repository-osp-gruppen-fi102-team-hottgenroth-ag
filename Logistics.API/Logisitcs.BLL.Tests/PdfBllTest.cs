using FluentAssertions;
using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Tests
{
    [TestFixture]
    public class PdfBllTest
    {
        private PDFBLL _pdfBll;
        private Mock<PdfHelper> _pdfHelperMock;
        private List<ITransportBoxData> _boxes;
        private Mock<IProjectData> _projectDataMock;

        [SetUp]
        public void Setup()
        {
            // Mock dependencies
            _pdfHelperMock = new Mock<PdfHelper>();
            _projectDataMock = new Mock<IProjectData>();
            _boxes = new List<ITransportBoxData>(); // Füge Mock-ITransportBoxData-Objekte hinzu

            // Initialisieren PDFBLL mit den gemockten Abhängigkeiten
            _pdfBll = new PDFBLL(_pdfHelperMock.Object);

            // Konfiguriere Mock
            _pdfHelperMock.Setup(p => p.Create(It.IsAny<List<ITransportBoxData>>(), It.IsAny<IProjectData>(), It.IsAny<List<IArticleData>>()))
                .ReturnsAsync(new byte[] { }); // Simulieren das Byte-Array, das von der Create-Methode zurückgegeben wird
        }

        [Test]
        public async Task Create_ReturnsNonNullByteArray()
        {
            var result = await _pdfBll.Create(_boxes, _projectDataMock.Object);

            // Null Check vom Byte Array
            result.Should().NotBeNull(); // Es MUSS ein Byte-Array zurück gegeben werden
        }

        [Test]
        public async Task Create_WithEmptyBoxList_ReturnsNonNullByteArray()
        {
            // Arrange
            var boxes = new List<ITransportBoxData>(); // Leere Liste
            var projectMock = new Mock<IProjectData>().Object;

            // Act
            var result = await _pdfBll.Create(boxes, projectMock);

            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public void Create_WithNullBoxList_ThrowsArgumentNullException()
        {
            // Arrange
            List<ITransportBoxData> boxes = null; // Null-Liste
            var projectMock = new Mock<IProjectData>().Object;

            // Act
            Func<Task> act = async () => await _pdfBll.Create(boxes, projectMock);

            // Assert
            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void Create_WithNullProject_ThrowsArgumentNullException()
        {
            // Arrange
            var boxes = new List<ITransportBoxData>(); // Kann leer oder gefüllt sein

            // Act
            Func<Task> act = async () => await _pdfBll.Create(boxes, null);

            // Assert
            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public async Task Create_CallsPdfHelperCreateExactlyOnce()
        {
            // Arrange
            var boxes = new List<ITransportBoxData>
            {
                new Mock<ITransportBoxData>().Object
            };
            var projectMock = new Mock<IProjectData>().Object;

            // Act
            await _pdfBll.Create(boxes, projectMock);

            // Assert
            _pdfHelperMock.Verify(p => p.Create(It.IsAny<List<ITransportBoxData>>(), It.IsAny<IProjectData>(), It.IsAny<List<IArticleData>>()), Times.Once);
        }
    }
}