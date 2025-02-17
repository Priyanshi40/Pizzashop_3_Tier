using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Review
{
    public int Reviewid { get; set; }

    public int? Customerid { get; set; }

    public string? Comments { get; set; }

    public int? Stars { get; set; }

    public string? Reviewfor { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
