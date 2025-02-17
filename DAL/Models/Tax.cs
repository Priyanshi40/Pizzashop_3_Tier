using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Tax
{
    public int TaxId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public double Amount { get; set; }

    public bool Isenabled { get; set; }

    public bool Defaulttax { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual ICollection<Ordertaxis> Ordertaxes { get; } = new List<Ordertaxis>();
}
