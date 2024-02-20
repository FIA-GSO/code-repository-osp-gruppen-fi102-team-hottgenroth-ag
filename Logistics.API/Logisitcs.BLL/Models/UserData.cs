using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System;

namespace Logisitcs.BLL.Models
{
   public class UserData : IUserData
   {
      public string UserEmail { get; set; }
      public Guid UserId { get; set; }
      public string Role { get; set; }
   }
}
