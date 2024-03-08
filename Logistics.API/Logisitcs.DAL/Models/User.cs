namespace Logisitcs.DAL.Models;

public partial class User
{
    public string UserId { get; set; }

    public string UserEmail { get; set; }

    public string UserPassword { get; set; }

    public int? UserRoleId { get; set; }

    public virtual UserRole UserRole { get; set; }
}