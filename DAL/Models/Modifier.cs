using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Modifier
{
    public int ModifierId { get; set; }

    public string Name { get; set; } = null!;

    public string Decription { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int Unit { get; set; }

    public bool Isdeleted { get; set; }

    public int? ModifiergroupId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();

    public virtual ICollection<Mainmodifier> Mainmodifiers { get; } = new List<Mainmodifier>();

    public virtual Modifiersgroup? Modifiergroup { get; set; }

    public virtual ICollection<Ordermodifier> Ordermodifiers { get; } = new List<Ordermodifier>();
}
