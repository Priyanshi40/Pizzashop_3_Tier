using System;
using System.Collections.Generic;

namespace DAL.Models;

public enum TableStatus{
    Available,
    Occupied
}
public partial class Table
{
    public int TableId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public TableStatus Status { get; set; } 

    public bool Isdeleted { get; set; }

    public int? SectionId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Customertable> Customertables { get; } = new List<Customertable>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Ordertable> Ordertables { get; } = new List<Ordertable>();

    public virtual Section? Section { get; set; }
}
