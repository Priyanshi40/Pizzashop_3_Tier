using System;
using System.Collections.Generic;

namespace DAL.Models;

public enum RoleName{
    Chef, AccountManager, SuperAdmin
}

public partial class Role
{
    public int RoleId { get; set; }

    public RoleName Role1 { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public string? Profileimage { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
