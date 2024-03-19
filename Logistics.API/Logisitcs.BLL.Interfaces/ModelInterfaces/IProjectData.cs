using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface IProjectData
    {
        DateTime CreationDate { get; set; }
        Guid ProjectGuid { get; set; }
        string ProjectName { get; set; }
    }
}