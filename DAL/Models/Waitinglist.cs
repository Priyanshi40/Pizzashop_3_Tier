using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Waitinglist
{
    public int Waitingid { get; set; }

    public DateTime Waitingtime { get; set; }

    public int? Customerid { get; set; }

    public int? SectionId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Section? Section { get; set; }
}
