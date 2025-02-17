using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int NoOfPerson { get; set; }

    public int? TableId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual ICollection<Customertable> Customertables { get; } = new List<Customertable>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual Table? Table { get; set; }

    public virtual ICollection<Waitinglist> Waitinglists { get; } = new List<Waitinglist>();
}
