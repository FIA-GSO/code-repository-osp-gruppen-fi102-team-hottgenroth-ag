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
                // GetUser from DB By Email Adress
                User user = DbCommandsUser.GetUserByMail(loginData.UserEmail);
                //Check if user exists and if the password is correct
                if (user != null)
                {
                    UserRole userRole = DbCommandsUser.GetUserRole((int)user.UserRoleId);
                    userData = new UserData(Guid.Parse(user.UserId), user.UserEmail, userRole.Role);
                    if (PasswordHashHelper.Verify(loginData.Password, user.UserPassword))
                    {
                        //Return User Data if Password is correct
                        return userData;
                    }
                }
                //Return null if User does not exist or Password is incorrect
                return null;
            });
        }

        //Register User
        public async Task<IUserData> Register(ILoginData loginData)
        {
            //Check if User with this Email already exists
            if (DbCommandsUser.GetUserByMail(loginData.UserEmail) != null)
            {
                throw new DuplicateNameException();
            }
            //Hash Password
            loginData.Password = PasswordHashHelper.Hash(loginData.Password);
            Guid userId = Guid.NewGuid();
            //Set User Role to User
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
                //Add User to DB
                DbCommandsUser.AddUser(userDAL);
                //Check if user was added
                User user = DbCommandsUser.GetUser(userId.ToString());
                UserRole userRole = DbCommandsUser.GetUserRole((int)user.UserRoleId);
                IUserData userData = null;
                if (user.UserId != null)
                {
                    userData = new UserData(userId, user.UserEmail, userRole.Role);
                }
                //If User was added return User Data else return null
                return userData;
            });
        }

        public async Task<bool> UpdateRole(IUserData user)
        {
            //Get User from DB
            User userDB = DbCommandsUser.GetUser(user.UserId.ToString());
            //Check if User exists
            if (userDB == null)
            {
                return false;
            }
            //Get User Role from DB ans set User Role Id
            int userRoleId = DbCommandsUser.GetUserRoleByName(user.Role);
            userDB.UserRoleId = userRoleId;
            //Update User Role
            DbCommandsUser.UpdateUser(userDB);
            return true;
        }

        public async Task<IEnumerable<IUserData>> GetAllUser()
        {
            return await Task.Run(() =>
            {
                //Get all Users from DB
                List<IUserData> result = new List<IUserData>();
                IEnumerable<User> usersDB = DbCommandsUser.GetAllUsers();
                //Check if Users exist
                if (usersDB == null)
                {
                    return null;
                }
                //Get User Role from DB and add User Data to List
                foreach (User user in usersDB)
                {
                    UserRole userRole = DbCommandsUser.GetUserRole((int)user.UserRoleId);
                    var userData = new UserData(Guid.Parse(user.UserId), user.UserEmail, userRole.Role);
                    result.Add(userData);
                }
                //Return List of User Data
                return result;
            });
        }
    }
}