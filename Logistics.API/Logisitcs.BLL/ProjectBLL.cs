using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Interfaces;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class ProjectBLL: IProjectBLL
    {
        private readonly ProjectDataFactory projectDataFactory;
        private readonly ProjectFactory projectFactory;
         public ProjectBLL()
         {
            projectDataFactory = new ProjectDataFactory();
            projectFactory = new ProjectFactory();
        }

        public async Task<IEnumerable<IProjectData>> GetAllProjects()
        {
            return await Task.Run(() =>
            {
                ProjectDataFactory projetDataFactory = new ProjectDataFactory();
                IEnumerable<Project> projects = DBCommands.GetAllProjects();
                IEnumerable<IProjectData> projectDatas = projects.Select(x => projetDataFactory.Create(x));
                return projectDatas;
            });
        }

        public async Task<IProjectData> GetProject(Guid guid)
        {
            return await Task.Run(() =>
            {
                Project project = DBCommands.GetProject(guid.ToString());
                IProjectData projectData = projectDataFactory.Create(project);
                return projectData;
                });
        }

        public async Task<IProjectData> AddProject(IProjectData projectData)
        {
            return await Task.Run(() =>
            {
                Project project = projectFactory.Create(projectData);
                DBCommands.AddProject(project);
                IProjectData projectDataResult = projectDataFactory.Create(project);
                return projectDataResult;
            });
        }
      
        public async Task<bool> UpdateProject(IProjectData projectData)
        {
            return await Task.Run(() =>
            {                
                var dbProject = DBCommands.GetProject(projectData.ProjectGuid.ToString());
                if(dbProject == null)
                {
                    return false;
                }
                Project project = projectFactory.Create(projectData);
                DBCommands.UpdateProject(project);
                return true;
            });
        }

        public async Task<bool> DeleteProject(Guid guid)
        {
            return await Task.Run(() =>
            {
                Project project = DBCommands.GetProject(guid.ToString());
                if(project == null)
                {
                    return false;
                }
                DBCommands.DeleteProject(guid.ToString());
                return true;
            });
        }
    }
}
