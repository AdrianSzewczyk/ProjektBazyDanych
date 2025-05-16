using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class Uzytkownicy
{
    public int IdUzytkownika { get; set; }

    public string? Imie { get; set; }

    public string? Nazwisko { get; set; }

    public string? Login { get; set; }

    public string? Haslo { get; set; }

    public DateTime Utworzony { get; set; }

    public int? IdRodzajKonta { get; set; }

    public virtual RodzajKontum? IdRodzajKontaNavigation { get; set; }

    public virtual ICollection<Rezerwacje> Rezerwacjes { get; set; } = new List<Rezerwacje>();
}
