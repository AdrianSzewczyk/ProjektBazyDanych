using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class RodzajKontum
{
    public int IdRodzajuKonta { get; set; }

    public string? Rodzaj { get; set; }

    public virtual ICollection<Uzytkownicy> Uzytkownicies { get; set; } = new List<Uzytkownicy>();
}
