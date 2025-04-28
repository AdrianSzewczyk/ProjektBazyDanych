using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class Ogloszenium
{
    public int IdOgloszenia { get; set; }

    public int? IdPojazdu { get; set; }

    public bool? Dostepnosc { get; set; }

    public byte[] DataDodania { get; set; } = null!;

    public decimal? Kwota { get; set; }

    public virtual Pojazd? IdPojazduNavigation { get; set; }

    public virtual ICollection<Rezerwacje> Rezerwacjes { get; set; } = new List<Rezerwacje>();
}
