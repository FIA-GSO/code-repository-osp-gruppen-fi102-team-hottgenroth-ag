using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface ILoginBLL
    {
      Task<IUserData> Login(ILoginData user);
      Task<IUserData> Register(ILoginData user);
      Task<bool> UpdateRole(IUserData user);
   }
}
