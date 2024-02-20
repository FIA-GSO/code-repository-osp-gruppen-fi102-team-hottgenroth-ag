using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.BLL.Models;
using Logisitcs.DAL;
using Logisitcs.DAL.Interfaces;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Logisitcs.BLL
{
    public class LoginBLL: ILoginBLL
    {
         public async Task<IUserData> Login(ILoginData loginData)
         {
            return await Task.Run(() =>
            {
                IUserData userData = null;
                User user = DBCommands.GetUserByMail(loginData.UserEmail);
                if (user.UserId != null)
                {
                    UserRole userRole = DBCommands.GetUserRole((int)user.UserRoleId);
                    userData = new UserData(Guid.Parse(user.UserId), user.UserEmail, userRole.Role);
                    if (PasswordHashHelper.Verify(loginData.Password, user.UserPassword))
                    {
                        return userData;
                    }
                }
               return null;
            });
        }

         public async Task<IUserData> Register(ILoginData user)
         {
            if(DBCommands.GetUserByMail(user.UserEmail)!= null)
            {
                throw new DuplicateNameException();
            }
            user.Password = PasswordHashHelper.Hash(user.Password);
            Guid userId = Guid.NewGuid();
            int userRoleId = 3;
            User userDAL = new()
            {
                UserId = userId.ToString(),
                UserEmail = user.UserEmail,
                UserPassword = user.Password,
                UserRoleId = userRoleId,                
            };
            return await Task.Run(() =>
            {
                DBCommands.AddUser(userDAL);
                User user = DBCommands.GetUser(userId.ToString());
                UserRole userRole = DBCommands.GetUserRole((int)user.UserRoleId);
                IUserData userData = null;
                if (user.UserId != null)
                {
                    userData = new UserData(userId, user.UserEmail, userRole.Role);
                }
                return userData;
            });
         }

         public async Task<bool> UpdateRole(IUserData user)
         {
            return true;
         }
    }
}
