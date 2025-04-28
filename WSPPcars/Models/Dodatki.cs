using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class Dodatki
{
    public int IdDodatku { get; set; }

    public string? Nazwa { get; set; }

    public string? LiczbaSztuk { get; set; }

    public bool? Dostepnosc { get; set; }

    public decimal? Kwota { get; set; }

    public virtual ICollection<DodatkiRezerwacje> DodatkiRezerwacjes { get; set; } = new List<DodatkiRezerwacje>();
}
