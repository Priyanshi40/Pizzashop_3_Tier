using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Ordertaxis
{
    public int Ordertaxid { get; set; }

    public int? OrderId { get; set; }

    public int? TaxId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Tax? Tax { get; set; }
}
