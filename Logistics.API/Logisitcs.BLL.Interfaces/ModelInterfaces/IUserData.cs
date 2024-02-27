using System;

namespace Logisitcs.BLL.Interfaces.ModelInterfaces
{
    public interface IUserData
    {
        string UserEmail { get; }
        Guid UserId { get; }
        string Role { get; }
    }
}