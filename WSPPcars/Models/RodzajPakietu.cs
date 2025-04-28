using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class RodzajPakietu
{
    public int IdRodzajPakietu { get; set; }

    public string? Pakiet { get; set; }

    public virtual ICollection<Ubezpieczenium> Ubezpieczenia { get; set; } = new List<Ubezpieczenium>();
}
