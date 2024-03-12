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
        public void Test_GetAllProjects()
        {
            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);
            //Liste aller vorhandener Projekte abholen
            IEnumerable<IProjectData> result = projectBll.GetAllProjects().Result;
            //Schauen ob alle Projekte vorhanden sind und Namen Prüfen
            result.Should().HaveCount(2);
            result.ElementAt(0).ProjectName.Should().Be("Madagascar");
            result.ElementAt(1).ProjectName.Should().Be("ABC");
        }

        [Test]
        public void Test_GetProjects()
        {
            Guid guid = Guid.Parse("18500286-ad03-4240-8ec8-ffe1d3a4e77d");

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);
            //Schauen ob ein vorhandenes Projekt abgeholt werden kann
            IProjectData result = projectBll.GetProject(guid).Result;
            result.ProjectName.Should().Be("Madagascar");
        }

        [Test]
        public void Test_AddProject_And_Delete()
        {
            DateTime expectedDate = DateTime.Now;
            Guid guid = Guid.NewGuid();
            string expectedProjectName = "Project Name";
            IProjectData projectData = new ProjectData()
            {
                CreationDate = expectedDate,
                ProjectGuid = guid,
                ProjectName = expectedProjectName
            };

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);
            //AddProject ausführen
            IProjectData result = projectBll.AddProject(projectData).Result;
            //Schauen ob die Properties richtig gesetzt wurden
            result.ProjectName.Should().Be(expectedProjectName);
            result.CreationDate.Should().Be(expectedDate);
            result.ProjectGuid.Should().Be(guid.ToString());
            //In der Datenbank schauen ob das neue Projekt angelegt wurde
            result = projectBll.GetProject(guid).Result;

            result.ProjectName.Should().Be(expectedProjectName);
            result.CreationDate.Should().Be(expectedDate);
            result.ProjectGuid.Should().Be(guid.ToString());
            //Aufräumen
            bool deleteResult = projectBll.DeleteProject(guid).Result;
            deleteResult.Should().BeTrue();
            //Prüfen ob aufräumen funktioniert hat
            result = projectBll.GetProject(guid).Result;
            result.Should().BeNull();
        }

        [Test]
        public void TestAdd_And_UpdateProject()
        {
            DateTime expectedDate = DateTime.Now;
            Guid guid = Guid.Parse("18500286-ad03-4240-8ec8-ffe1d3a4e77d");
            string projectName = "Madagascar";
            IProjectData projectData = new ProjectData()
            {
                CreationDate = expectedDate,
                ProjectGuid = guid,
                ProjectName = projectName
            };

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);

            //AddProject mit Guid die schon vorhanden ist soll eine Exception werfen
            projectBll.AddProject(projectData).Invoking(x => x.Result).Should().Throw<Exception>();

            //setzen einer neuen Guid und danach ein AddProject ausführen
            Guid expectedNewGuid = Guid.NewGuid();
            projectData.ProjectGuid = expectedNewGuid;
            IProjectData addedProject = projectBll.AddProject(projectData).Result;
            addedProject.ProjectGuid.Should().Be(expectedNewGuid);

            //Namen umsetzen auf neuen Namen und UpdateProject
            string newProject = "Neues Projekt";
            projectData.ProjectName = newProject;
            bool updateResult = projectBll.UpdateProject(projectData).Result;
            updateResult.Should().BeTrue();

            //Prüfen ob ProjectName geändert wurde
            IProjectData updatedResult = projectBll.GetProject(expectedNewGuid).Result;
            updatedResult.ProjectName.Should().Be(newProject);

            //Löschen des neuen Projekts
            bool deleteResult = projectBll.DeleteProject(expectedNewGuid).Result;
            deleteResult.Should().BeTrue();
        }

        [Test]
        public void TestUpdateProject_With_Wrong_Guid_Should_Return_False()
        {
            DateTime expectedDate = DateTime.Now;
            Guid wrongGuid = Guid.Parse("18500286-0000-4240-8ec8-ffe1d3a4e77d");
            string projectName = "Madagascar";
            IProjectData projectData = new ProjectData()
            {
                CreationDate = expectedDate,
                ProjectGuid = wrongGuid,
                ProjectName = projectName
            };

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);
            //Update des Projektes mit nicht vorhanden DB Eintrag sollte false ausgeben
            bool result = projectBll.UpdateProject(projectData).Result;
            result.Should().BeFalse();
        }

        [Test]
        public void TestDeleteProject_Should_Return_False()
        {
            Guid wrongGuid = Guid.Parse("18500286-0000-4240-8ec8-ffe1d3a4e77d");

            IProjectBll projectBll = new ProjectBll(projectDataFactory, projectFactory);
            //Delete des Projektes mit nicht vorhanden DB Eintrag sollte false ausgeben
            bool deleteResult = projectBll.DeleteProject(wrongGuid).Result;
            deleteResult.Should().BeFalse();
        }
    }
}