using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Section
{
    public int SectionId { get; set; }

    public string Name { get; set; } = null!;

    public string Decription { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public virtual ICollection<Table> Tables { get; } = new List<Table>();

    public virtual ICollection<Waitinglist> Waitinglists { get; } = new List<Waitinglist>();
}
