using System;
using System.Collections.Generic;

namespace DAL.Models;

public enum PaymentStatus{
    UPI,
    Card,
    Cash,
    Pending
}

public enum PaymentMode{
    Completed,
    Failed
}

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public int? Customerid { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public PaymentMode Paymentmode { get; set; }

    public PaymentStatus Paymentstatus { get; set; }

    public virtual Customer? Customer { get; set; }
}
