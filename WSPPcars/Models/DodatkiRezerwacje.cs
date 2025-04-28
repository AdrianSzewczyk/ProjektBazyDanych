using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class DodatkiRezerwacje
{
    public int IdDodatkuRezerwacji { get; set; }

    public int? IdRezerwacji { get; set; }

    public int? IdDodatku { get; set; }

    public virtual Dodatki? IdDodatkuNavigation { get; set; }

    public virtual Rezerwacje? IdRezerwacjiNavigation { get; set; }
}
