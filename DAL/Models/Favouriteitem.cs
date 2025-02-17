using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Favouriteitem
{
    public int Favouriteid { get; set; }

    public int? ItemId { get; set; }

    public int? UserId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? User { get; set; }
}
