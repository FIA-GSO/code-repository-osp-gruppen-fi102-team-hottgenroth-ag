using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface ILoginBll
    {
        Task<IUserData> Login(ILoginData loginData);

        Task<IUserData> Register(ILoginData loginData);

        Task<bool> UpdateRole(IUserData user);
    }
}