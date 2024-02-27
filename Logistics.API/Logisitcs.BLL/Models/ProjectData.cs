using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public class ProjectData : IProjectData
    {
        public Guid ProjectGuid { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
