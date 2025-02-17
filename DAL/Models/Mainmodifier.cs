using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Mainmodifier
{
    public int MainmodifierId { get; set; }

    public int? ModifierId { get; set; }

    public int? ModifiergroupId { get; set; }

    public virtual Modifier? Modifier { get; set; }

    public virtual Modifiersgroup? Modifiergroup { get; set; }
}
