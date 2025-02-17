using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Permission
{
    public bool? Canview { get; set; }

    public bool Canedit { get; set; }

    public bool Candelete { get; set; }

    public int? RoleId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public string PageName { get; set; } = null!;

    public virtual Role? Role { get; set; }
}
