using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class Ubezpieczenium
{
    public int IdUbezpieczenia { get; set; }

    public string? Nazwa { get; set; }

    public string? NazwaUbezpieczalni { get; set; }

    public int? IdRodzajPakietu { get; set; }

    public decimal? Kwota { get; set; }

    public bool? Dostepnosc { get; set; }

    public virtual RodzajPakietu? IdRodzajPakietuNavigation { get; set; }

    public virtual ICollection<Rezerwacje> Rezerwacjes { get; set; } = new List<Rezerwacje>();

    public override string ToString()
    {
        return $"{this.NazwaUbezpieczalni}: {this.Nazwa}: {this.Kwota}PLN ({this.IdRodzajPakietuNavigation.Pakiet})";
    }
    }
