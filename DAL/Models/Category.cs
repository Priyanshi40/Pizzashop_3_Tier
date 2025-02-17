using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Decription { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();

    public virtual ICollection<Modifiersgroup> Modifiersgroups { get; } = new List<Modifiersgroup>();
}
