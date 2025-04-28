using System;
using System.Collections.Generic;

namespace WSPPCars.Models;

public partial class Rezerwacje
{
    public int IdRezerwacji { get; set; }

    public int? IdOgloszenia { get; set; }

    public int? IdUbezpieczenia { get; set; }

    public int? IdUzytkownika { get; set; }

    public int? IdStanRezerwacji { get; set; }

    public DateTime? DataRozpoczeciaRezerwacji { get; set; }

    public DateTime? DataZakonczeniaRezerwacji { get; set; }

    public byte[] Utworzona { get; set; } = null!;

    public decimal? KwotaUbezpieczenia { get; set; }

    public decimal? KwotaDodatku { get; set; }

    public decimal? KwotaOgloszenia { get; set; }

    public decimal? KwotaRezerwacji { get; set; }

    public bool? DostepnoscPojazdu { get; set; }

    public virtual ICollection<DodatkiRezerwacje> DodatkiRezerwacjes { get; set; } = new List<DodatkiRezerwacje>();

    public virtual Ogloszenium? IdOgloszeniaNavigation { get; set; }

    public virtual StanRezerwacji? IdStanRezerwacjiNavigation { get; set; }

    public virtual Ubezpieczenium? IdUbezpieczeniaNavigation { get; set; }

    public virtual Uzytkownicy? IdUzytkownikaNavigation { get; set; }
}
