using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class StanRezerwacji
{
    public int IdStanuRezerwacji { get; set; }

    public string? Stan { get; set; }

    public virtual ICollection<Rezerwacje> Rezerwacjes { get; set; } = new List<Rezerwacje>();
}
