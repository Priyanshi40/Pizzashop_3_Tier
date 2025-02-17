using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Modifiersgroup
{
    public int ModifiergroupId { get; set; }

    public string Name { get; set; } = null!;

    public string Decription { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public int? CategoryId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Mainmodifier> Mainmodifiers { get; } = new List<Mainmodifier>();

    public virtual ICollection<Modifier> Modifiers { get; } = new List<Modifier>();
}
