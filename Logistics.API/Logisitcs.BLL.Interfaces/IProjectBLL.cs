using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface IProjectBLL
    {
        Task<IEnumerable<IProjectData>> GetAllProjects();
        Task<IProjectData> GetProject(Guid guid);
        Task<IProjectData> AddProject(IProjectData projectData);
        Task<bool> UpdateProject(IProjectData projectData);
        Task<bool> DeleteProject(Guid guid);
    }
}
