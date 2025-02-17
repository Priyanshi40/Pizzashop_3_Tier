using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string Decription { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int Unit { get; set; }

    public bool? Isavailable { get; set; }

    public bool Defaulttax { get; set; }

    public decimal Additionaltax { get; set; }

    public string Code { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public string? Image { get; set; }

    public int? CategoryId { get; set; }

    public int? ModifierId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Favouriteitem> Favouriteitems { get; } = new List<Favouriteitem>();

    public virtual Modifier? Modifier { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();
}
