using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.BLL.Models;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class LoginBll : ILoginBll
    {
        public async Task<IUserData> Login(ILoginData loginData)
        {
            return await Task.Run(() =>
            {
                IUserData userData = null;
                User user = DbCommandsUser.GetUserByMail(loginData.UserEmail);
                if (user != null)
                {
                    UserRole userRole = DbCommandsUser.GetUserRole((int)user.UserRoleId);
                    userData = new UserData(Guid.Parse(user.UserId), user.UserEmail, userRole.Role);
                    if (PasswordHashHelper.Verify(loginData.Password, user.UserPassword))
                    {
                        return userData;
                    }
                }
                return null;
            });
        }

        public async Task<IUserData> Register(ILoginData loginData)
        {
            if (DbCommandsUser.GetUserByMail(loginData.UserEmail) != null)
            {
                throw new DuplicateNameException();
            }
            loginData.Password = PasswordHashHelper.Hash(loginData.Password);
            Guid userId = Guid.NewGuid();
            int userRoleId = 5;
            User userDAL = new()
            {
                UserId = userId.ToString(),
                UserEmail = loginData.UserEmail,
                UserPassword = loginData.Password,
                UserRoleId = userRoleId,
            };
            return await Task.Run(() =>
            {
                DbCommandsUser.AddUser(userDAL);
                User user = DbCommandsUser.GetUser(userId.ToString());
                UserRole userRole = DbCommandsUser.GetUserRole((int)user.UserRoleId);
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
            User userDB = DbCommandsUser.GetUser(user.UserId.ToString());
            if (userDB == null)
            {
                return false;
            }
            int userRoleId = DbCommandsUser.GetUserRoleByName(user.Role);
            userDB.UserRoleId = userRoleId;
            DbCommandsUser.UpdateUser(userDB);
            return true;
        }

        public async Task<IEnumerable<IUserData>> GetAllUser()
        {
            return await Task.Run(() =>
            {
                List<IUserData> result = new List<IUserData>();
                IEnumerable<User> usersDB = DbCommandsUser.GetAllUsers();
                if (usersDB == null)
                {
                    return null;
                }
                foreach (User user in usersDB)
                {
                    UserRole userRole = DbCommandsUser.GetUserRole((int)user.UserRoleId);
                    var userData = new UserData(Guid.Parse(user.UserId), user.UserEmail, userRole.Role);
                    result.Add(userData);
                }

                return result;
            });
        }
    }
}