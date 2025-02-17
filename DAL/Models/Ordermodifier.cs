using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Ordermodifier
{
    public int Ordermodifierid { get; set; }

    public int? OrderId { get; set; }

    public int? ModifierId { get; set; }

    public virtual Modifier? Modifier { get; set; }

    public virtual Order? Order { get; set; }
}
