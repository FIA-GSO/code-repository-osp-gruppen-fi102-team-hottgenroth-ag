using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface IUserData
    {
        string UserEmail { get; set; }
        Guid UserId { get; set; }
        string Role { get; set; }
    }
}