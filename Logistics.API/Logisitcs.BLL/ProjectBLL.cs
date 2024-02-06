using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;
using System;

namespace Logisitcs.BLL
{
    public class ProjectBLL: IProjectBLL
    {
         IProjectDAL _DAL;
         public ProjectBLL(IProjectDAL dal)
         {
            _DAL = dal;
         }
    }
}
