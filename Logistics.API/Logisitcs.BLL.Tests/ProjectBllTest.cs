using FluentAssertions;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using NUnit.Framework;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Logisitcs.BLL.Factories;

namespace Logisitcs.BLL.Tests
{
    [TestFixture]
    public class ProjectBllTest
    {
        private IProjectDataFactory projectDataFactory;
        private IProjectFactory projectFactory;

        [SetUp]
        public void Setup()
        {
            projectDataFactory = new ProjectDataFactory();
            projectFactory = new ProjectFactory();
        }

        [Test]
        public void TestGetAllProjects()
        {
            // Arrange
            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);
            // Act
            IEnumerable<IProjectData> result = projectBll.GetAllProjects().Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.ElementAt(0).ProjectName.Should().Be("Madagascar");
            result.ElementAt(1).ProjectName.Should().Be("ABC");
        }

        [Test]
        public void TestGetProjects()
        {
            Guid guid = Guid.Parse("18500286-ad03-4240-8ec8-ffe1d3a4e77d");

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);

            IProjectData result = projectBll.GetProject(guid).Result;
            result.ProjectName.Should().Be("Madagascar");
        }

        [Test]
        public void TestAddProject()
        {
            var expectedDate = DateTime.Now;
            var guid = Guid.NewGuid();
            var expectedProjectName = "Project Name";
            IProjectData projectData = new ProjectData()
            {
                CreationDate = expectedDate,
                ProjectGuid = guid,
                ProjectName = expectedProjectName
            };

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);

            IProjectData result = projectBll.AddProject(projectData).Result;

            result.ProjectName.Should().Be(expectedProjectName);
            result.CreationDate.Should().Be(expectedDate);
            result.ProjectGuid.Should().Be(guid.ToString());

            result = projectBll.GetProject(guid).Result;

            result.ProjectName.Should().Be(expectedProjectName);
            result.CreationDate.Should().Be(expectedDate);
            result.ProjectGuid.Should().Be(guid.ToString());

            bool deleteResult = projectBll.DeleteProject(guid).Result;
            deleteResult.Should().BeTrue();

            result = projectBll.GetProject(guid).Result;
            result.Should().BeNull();
        }

        [Test]
        public void TestUpdateProject()
        {
        }
    }
}