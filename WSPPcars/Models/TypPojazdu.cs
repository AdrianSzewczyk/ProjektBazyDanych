using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class TypPojazdu
{
    public int IdTypuPojazdu { get; set; }

    public string? Typ { get; set; }

    public virtual ICollection<PojazdSztuka> PojazdSztukas { get; set; } = new List<PojazdSztuka>();
}
