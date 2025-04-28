using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class Pojazd
{
    public int IdPojazdu { get; set; }

    public int? IdSztuki { get; set; }

    public int? LiczbaSztuk { get; set; }

    public virtual PojazdSztuka? IdSztukiNavigation { get; set; }

    public virtual ICollection<Ogloszenium> Ogloszenia { get; set; } = new List<Ogloszenium>();
}
