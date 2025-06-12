using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class PojazdSztuka
{
    public int IdPojazdSztuka { get; set; }

    public string? Marka { get; set; }

    public string? Model { get; set; }

    public int? IdTypPojazdu { get; set; }

    public decimal? PojemnoscSilnika { get; set; }

    public int? LiczbaDrzwi { get; set; }

    public int? LiczbaPasazerow { get; set; }

    public bool? AutomatycznaSkrzynia { get; set; }

    public DateOnly? Rocznik { get; set; }

    public String? Zdjecie {  get; set; }

    public virtual TypPojazdu? IdTypPojazduNavigation { get; set; }

    public virtual ICollection<Pojazd> Pojazds { get; set; } = new List<Pojazd>();
}
