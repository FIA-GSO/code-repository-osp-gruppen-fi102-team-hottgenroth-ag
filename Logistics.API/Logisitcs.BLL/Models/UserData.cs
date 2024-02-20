using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;

namespace Logisitcs.BLL.Models
{
   public class UserData : IUserData
   {
        public UserData(Guid userId, string userEmail, string role)
        {
            UserEmail = userEmail;
            UserId = userId;
            Role = role;
        }

        public string UserEmail { get; }
        public Guid UserId { get;  }
        public string Role { get; }
   }
}
