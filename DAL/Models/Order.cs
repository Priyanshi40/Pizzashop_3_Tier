using System;
using System.Collections.Generic;

namespace DAL.Models;

public enum OrderStatus{
    Pending,
    Completed,
    InProgress,
    Running
}

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? Orderdate { get; set; }

    public OrderStatus Status { get; set; } 

    public PaymentStatus Paymentmode { get; set; } 

    public decimal Totalamount { get; set; }

    public string? Comment { get; set; }

    public int? Reviewid { get; set; }

    public int? Customerid { get; set; }

    public int? TableId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();

    public virtual ICollection<Ordermodifier> Ordermodifiers { get; } = new List<Ordermodifier>();

    public virtual ICollection<Ordertable> Ordertables { get; } = new List<Ordertable>();

    public virtual ICollection<Ordertaxis> Ordertaxes { get; } = new List<Ordertaxis>();

    public virtual Review? Review { get; set; }

    public virtual Table? Table { get; set; }
}
