using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Orderitem
{
    public int Orderitemid { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int Quantity { get; set; }

    public string? Instructions { get; set; }

    public int Readyitems { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Order? Order { get; set; }
}
