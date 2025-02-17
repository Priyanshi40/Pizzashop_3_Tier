using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class City
{
    public int CityId { get; set; }

    public string Cityname { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual State? State { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
