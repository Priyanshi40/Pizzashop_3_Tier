using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Ordertable
{
    public int Ordertableid { get; set; }

    public int? OrderId { get; set; }

    public int? TableId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Table? Table { get; set; }
}
