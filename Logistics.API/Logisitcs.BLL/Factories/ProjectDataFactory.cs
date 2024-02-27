using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;
using System;

namespace Logisitcs.BLL.Factories
{
    public class ProjectDataFactory : IProjectDataFactory
    {
        public IProjectData Create(Project project)
        {
            return new ProjectData
            {
                ProjectGuid = Guid.Parse(project.ProjectGuid),
                CreationDate = project.CreationDate,
                ProjectName = project.ProjectName
            };

        }
    }
}
