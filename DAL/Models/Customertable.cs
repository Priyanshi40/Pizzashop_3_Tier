using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Customertable
{
    public int Customertableid { get; set; }

    public int? Customerid { get; set; }

    public int? TableId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Table? Table { get; set; }
}
