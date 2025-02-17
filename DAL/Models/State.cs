using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class State
{
    public int StateId { get; set; }

    public string Statename { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
