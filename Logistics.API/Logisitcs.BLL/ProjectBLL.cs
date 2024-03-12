using Logisitcs.BLL.Factories;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class ProjectBll : IProjectBll
    {
        private readonly IProjectDataFactory projectDataFactory;
        private readonly IProjectFactory projectFactory;

        public ProjectBll(IProjectDataFactory projectDataFactory, IProjectFactory projectFactory)
        {
            this.projectDataFactory = projectDataFactory;
            this.projectFactory = projectFactory;
        }

        public async Task<IEnumerable<IProjectData>> GetAllProjects()
        {
            return await Task.Run(() =>
            {
                IEnumerable<Project> projects = DbCommandsProject.GetAllProjects();
                IEnumerable<IProjectData> projectDatas = projects.Select(x => projectDataFactory.Create(x));
                return projectDatas;
            });
        }

        public async Task<IProjectData> GetProject(Guid guid)
        {
            return await Task.Run(() =>
            {
                Project project = DbCommandsProject.GetProject(guid.ToString());
                if (project != null)
                {
                    IProjectData projectData = projectDataFactory.Create(project);
                    return projectData;
                }
                return null;
            });
        }

        public async Task<IProjectData> AddProject(IProjectData projectData)
        {
            return await Task.Run(() =>
            {
                Project project = projectFactory.Create(projectData);
                DbCommandsProject.AddProject(project);
                IProjectData projectDataResult = projectDataFactory.Create(project);
                return projectDataResult;
            });
        }

        public async Task<bool> UpdateProject(IProjectData projectData)
        {
            return await Task.Run(() =>
            {
                Project dbProject = DbCommandsProject.GetProject(projectData.ProjectGuid.ToString());
                if (dbProject == null)
                {
                    return false;
                }
                Project project = projectFactory.Create(projectData);
                DbCommandsProject.UpdateProject(project);
                return true;
            });
        }

        public async Task<bool> DeleteProject(Guid guid)
        {
            return await Task.Run(() =>
            {
                Project project = DbCommandsProject.GetProject(guid.ToString());
                if (project == null)
                {
                    return false;
                }
                DbCommandsProject.DeleteProject(guid.ToString());
                return true;
            });
        }
    }
}