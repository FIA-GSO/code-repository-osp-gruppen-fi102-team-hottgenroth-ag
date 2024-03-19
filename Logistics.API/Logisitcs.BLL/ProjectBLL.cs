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

        //Get a List of AllProjects in DB
        public async Task<IEnumerable<IProjectData>> GetAllProjects()
        {
            return await Task.Run(() =>
            {
                IEnumerable<Project> projects = DbCommandsProject.GetAllProjects();
                IEnumerable<IProjectData> projectDatas = projects.Select(x => projectDataFactory.Create(x));
                return projectDatas;
            });
        }

        //Get one project by projectGuid
        public async Task<IProjectData> GetProject(Guid projectGuid)
        {
            return await Task.Run(() =>
            {
                //Search for Project in DB
                Project project = DbCommandsProject.GetProject(projectGuid.ToString());
                //If Project ist founded Create IProjectData and return
                if (project != null)
                {
                    IProjectData projectData = projectDataFactory.Create(project);
                    return projectData;
                }
                //Should the Project be null return null
                return null;
            });
        }

        public async Task<IProjectData> AddProject(IProjectData projectData)
        {
            return await Task.Run(() =>
            {
                // Map projectData to Project DB Class
                Project project = projectFactory.Create(projectData);
                DbCommandsProject.AddProject(project);
                // Map project DB Class to projectData for response
                IProjectData projectDataResult = projectDataFactory.Create(project);
                return projectDataResult;
            });
        }

        public async Task<bool> UpdateProject(IProjectData projectData)
        {
            return await Task.Run(() =>
            {
                //Check if projectData is in DB
                Project dbProject = DbCommandsProject.GetProject(projectData.ProjectGuid.ToString());
                //Return false if Project is not in DB
                if (dbProject == null)
                {
                    return false;
                }
                //Else Map projektData to Project DB and Update it
                Project project = projectFactory.Create(projectData);
                DbCommandsProject.UpdateProject(project);
                //Return True after UpdateProject
                return true;
            });
        }

        public async Task<bool> DeleteProject(Guid projectGuid)
        {
            return await Task.Run(() =>
            {
                //Check if projectData is in DB
                Project project = DbCommandsProject.GetProject(projectGuid.ToString());
                //Return false if Project is not in DB
                if (project == null)
                {
                    return false;
                }
                DbCommandsProject.DeleteProject(projectGuid.ToString());
                //Return true after Projekt is removed from DB
                return true;
            });
        }
    }
}