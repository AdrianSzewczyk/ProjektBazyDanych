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

    public partial class wynajemSzczegoly : Window
    {
        private Ogloszenium carAd;
        private Uzytkownicy aktualnyUzytkownik;
        private DateTime? dataWypozyczenia;
        private DateTime? dataZwrotu;
        public wynajemSzczegoly()
        {
            InitializeComponent();
            WyswietlDodatki();
            WyswietlUbezpieczenia();
        }

        public wynajemSzczegoly(Ogloszenium carAd, Uzytkownicy aktualnyUzytkownik, DateTime? dataWypozyczenia, DateTime? dataZwrotu)
        {
            this.aktualnyUzytkownik = aktualnyUzytkownik;
            this.carAd = carAd;
            this.dataWypozyczenia = dataWypozyczenia;
            this.dataZwrotu = dataZwrotu;
            InitializeComponent();
            WyswietlDodatki();
            WyswietlUbezpieczenia();
            
        }
        /* private void WyswietlDodatki()
         {
             listDodatki.Items.Clear();
             using (var context = new DbWsppcarsContext())
             {
                 var ogloszenia = context.Dodatkis.ToList();
                 foreach (var o in ogloszenia) {
                     listDodatki.Items.Add(o);
                 }

             }
         }*/

        public bool CzyRezerwacjaMozliwa(DateTime? poczatekRezerwacji, DateTime? koniecRezerwacji,
                                  DateTime? poczatekOkresu, DateTime? koniecOkresu)
        {
            bool pokrywaSie = poczatekRezerwacji < koniecOkresu && koniecRezerwacji > poczatekOkresu;
            return !pokrywaSie;
        }

        private void WyswietlDodatki()
        {
            listDodatki.Items.Clear();

            var dataPocz = dataWypozyczenia;
            var dataKoniec = dataZwrotu;

            if (dataPocz == null || dataKoniec == null)
            {
                MessageBox.Show("Wybierz daty wypożyczenia i zwrotu.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var context = new DbWsppcarsContext())
            {
                // Pobierz wszystkie dodatki
                var wszystkieDodatki = context.Dodatkis.ToList();

                // Pobierz rezerwacje z przypisanymi dodatkami i datami
                var rezerwacjeZDodatkami = context.DodatkiRezerwacjes
                    .Include(dr => dr.IdRezerwacjiNavigation)
                    .ToList();

                var dostepneDodatki = new List<Dodatki>();

                foreach (var dodatek in wszystkieDodatki)
                {
                    int liczbaSztuk = int.TryParse(dodatek.LiczbaSztuk, out int ilosc) ? ilosc : 0;

                    // Zlicz zajętość tego dodatku w kolidujących rezerwacjach
                    int zajeteSztuki = rezerwacjeZDodatkami
                        .Where(dr =>
                            dr.IdDodatku == dodatek.IdDodatku &&
                            !CzyRezerwacjaMozliwa(
                                dataPocz,
                                dataKoniec,
                                dr.IdRezerwacjiNavigation.DataRozpoczeciaRezerwacji,
                                dr.IdRezerwacjiNavigation.DataZakonczeniaRezerwacji
                            )
                        )
                        .Count();

                    if (zajeteSztuki < liczbaSztuk)
                    {
                        dostepneDodatki.Add(dodatek);
                    }
                }

                // Dodaj do widoku tylko dostępne dodatki
                foreach (var d in dostepneDodatki)
                {
                    listDodatki.Items.Add(d);
                }
            }
        }

        private void WyswietlUbezpieczenia()
        {
            comboUbezpieczenie.Items.Clear();
            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenia = context.Ubezpieczenia.Include(o => o.IdRodzajPakietuNavigation).ToList();
                foreach (var u in ubezpieczenia)
                {
                    comboUbezpieczenie.Items.Add(u);
                }

            }
        }

        private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            Ubezpieczenium u = (Ubezpieczenium)comboUbezpieczenie.SelectedItem;
            List<Dodatki> d = listDodatki.SelectedItems.Cast<Dodatki>().ToList();
            Rezerwacja rez = new Rezerwacja(carAd,u, aktualnyUzytkownik, dataWypozyczenia, dataZwrotu, d);
            rez.Owner = this;
            rez.WindowStartupLocation = WindowStartupLocation.Manual;
            rez.Width = this.Width;
            rez.Height = this.Height;
            rez.Left = this.Left;
            rez.Top = this.Top;
            this.Hide();
            rez.ShowDialog();
            this.Close();
        }

        private void listModeleAut_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}