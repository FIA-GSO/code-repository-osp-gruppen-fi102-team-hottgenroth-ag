﻿using System.Collections.Generic;

namespace Logisitcs.DAL.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string Role { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}