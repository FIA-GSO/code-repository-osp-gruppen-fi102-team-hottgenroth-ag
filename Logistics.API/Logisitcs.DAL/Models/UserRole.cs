using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class UserRole
{
    public long RoleId { get; set; }

    public string Role { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}