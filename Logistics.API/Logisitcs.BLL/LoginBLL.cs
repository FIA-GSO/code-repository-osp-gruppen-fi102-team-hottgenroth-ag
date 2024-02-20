using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.BLL.Models;
using Logisitcs.DAL.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Logisitcs.BLL
{
    public class LoginBLL: ILoginBLL
    {
         
         public LoginBLL()
         {
            
         }

         public async Task<IUserData> Login(ILoginData user)
         { 
            return await AuthenticateUser(user);
         }

         public async Task<IUserData> Register(ILoginData user)
         {
            user.Password = PasswordHashHelper.Hash(user.Password);
            //add user to table and create standard role and userId
            return new UserData();
         }

         public async Task<bool> UpdateRole(IUserData user)
         {
            return true;
         }

         private async Task<IUserData> AuthenticateUser(ILoginData login)
         {
            IUserData user = null;

            
            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information
            if (login.UserEmail == "r.mueller@hottgenroth.de")
            {
               user = new UserData { UserEmail = "r.mueller@hottgenroth.de", Role = "Lagerist", UserId = new Guid() };
            }
            //check ob password korrekt ist und return dann die UserData (Email, Role, UserId)
            //if (PasswordHashHelper.Verify(login.Password, hashedPassword)
            return user;
         }
    }
}
