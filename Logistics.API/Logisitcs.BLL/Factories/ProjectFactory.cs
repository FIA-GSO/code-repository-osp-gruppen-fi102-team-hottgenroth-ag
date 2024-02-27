using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL.Models;

namespace Logisitcs.BLL.Factories
{
    public class ProjectFactory : IProjectFactory
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