using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;
using System;

namespace Logisitcs.BLL
{
    public class ProjectDataFactory
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
    public class ProjectFactory
    {
        public Project Create(IProjectData projectData)
        {
            return new Project
            {
                ProjectGuid = projectData.ProjectGuid.ToString(),
                CreationDate = projectData.CreationDate,
                ProjectName = projectData.ProjectName
            };
        }
    }
}
