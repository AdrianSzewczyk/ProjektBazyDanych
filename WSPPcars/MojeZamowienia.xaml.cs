using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WSPPCars.Models;

namespace WSPPCars
{
    public class RezerwacjaWidok
    {
        public int IdRezerwacji { get; set; }
        public int? IdUzytkownika { get; set; }
        public int? IdOgloszenia { get; set; }

        public string? Samochod { get; set; }//
        public string? IdStanRezerwacji { get; set; }

        public DateTime? DataRozpoczeciaRezerwacji { get; set; }
        public DateTime? DataZakonczeniaRezerwacji { get; set; }

        public decimal? KwotaUbezpieczenia { get; set; }
        public string? Ubezpieczenie { get; set; }//
        public decimal? KwotaDodatku { get; set; }
        public decimal? KwotaOgloszenia { get; set; }
        public decimal? KwotaRezerwacji { get; set; }
    }

    public partial class MojeZamowienia : Window
    {

        public MojeZamowienia()
        {
            InitializeComponent();
        }

        public MojeZamowienia(Uzytkownicy aktualnyUzytkownik)
        {
            InitializeComponent();
            using var db = new DbWsppcarsContext();
            {
                var listaWidokowa = db.Rezerwacjes.Include(r => r.IdStanRezerwacjiNavigation)
                    .Include(r => r.IdUbezpieczeniaNavigation)
                    .ThenInclude(r => r.IdRodzajPakietuNavigation)
                    .Include(r => r.IdOgloszeniaNavigation).ThenInclude(r => r.IdPojazduNavigation).ThenInclude(r => r.IdSztukiNavigation)
                    .Select(r => new RezerwacjaWidok
                {
                    IdRezerwacji = r.IdRezerwacji,
                    IdUzytkownika = r.IdUzytkownika,
                    IdOgloszenia = r.IdOgloszenia,
                    Samochod = $"{r.IdOgloszeniaNavigation.IdPojazduNavigation.IdSztukiNavigation.Marka} {r.IdOgloszeniaNavigation.IdPojazduNavigation.IdSztukiNavigation.Model}",
                    IdStanRezerwacji = r.IdStanRezerwacjiNavigation.Stan,
                    DataRozpoczeciaRezerwacji = r.DataRozpoczeciaRezerwacji,
                    DataZakonczeniaRezerwacji = r.DataZakonczeniaRezerwacji,
                    Ubezpieczenie = $"{r.IdUbezpieczeniaNavigation.NazwaUbezpieczalni}: {r.IdUbezpieczeniaNavigation.Nazwa} ({r.IdUbezpieczeniaNavigation.IdRodzajPakietuNavigation.Pakiet})",
                    KwotaUbezpieczenia = r.KwotaUbezpieczenia,
                    KwotaDodatku = r.KwotaDodatku,
                    KwotaOgloszenia = r.KwotaOgloszenia,
                    KwotaRezerwacji = r.KwotaRezerwacji
                })
                    .Where(o => o.IdUzytkownika == aktualnyUzytkownik.IdUzytkownika)
                    .ToList();
                /*var rezerwacje = db.Rezerwacjes
                               .Where(o => o.IdUzytkownika == aktualnyUzytkownik.IdUzytkownika)
                               .ToList();*/

                ZamowieniaListView.ItemsSource = listaWidokowa;
            }
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class Zamowienie
    {
        public string NumerZamowienia { get; set; }
        public string DataZamowienia { get; set; }
        public string Status { get; set; }
    }
}
