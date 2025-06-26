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
    public partial class wynajemSamochodu : Window
    {
        private Uzytkownicy aktualnyUzytkownik;
        private Ogloszenium carAd;
        public wynajemSamochodu()
        {
            InitializeComponent();
        }

        public bool CzyRezerwacjaMozliwa(DateTime? poczatekRezerwacji, DateTime? koniecRezerwacji,
                                  DateTime? poczatekOkresu, DateTime? koniecOkresu)
        {
            bool pokrywaSie = poczatekRezerwacji < koniecOkresu && koniecRezerwacji > poczatekOkresu;
            return !pokrywaSie;
        }

        public wynajemSamochodu(Ogloszenium carAd, Uzytkownicy aktualnyUzytkownik)
        {
            InitializeComponent();
            this.carAd = carAd;
            this.aktualnyUzytkownik = aktualnyUzytkownik;
        }

        private void BtnPrzejdz_Click(object sender, RoutedEventArgs e)
        {
            var dataWypozyczenia = dpDataWypozyczenia.SelectedDate;
            var dataZwrotu = dpDataZwrotu.SelectedDate;

            if (dataWypozyczenia == null || dataZwrotu == null)
            {
                lblKomunikat.Content = "Proszę wybrać daty wypożyczenia i zwrotu.";
                lblKomunikat.Foreground = Brushes.OrangeRed;
                lblKomunikat.Visibility = Visibility.Visible;
                return;
            }

            if (dataZwrotu <= dataWypozyczenia)
            {
                lblKomunikat.Content = "Data zwrotu musi być późniejsza niż data wypożyczenia.";
                lblKomunikat.Foreground = Brushes.OrangeRed;
                lblKomunikat.Visibility = Visibility.Visible;
                return;
            }

            

            //#################

            using var db = new DbWsppcarsContext();

            
            var dataPocz = dpDataWypozyczenia.SelectedDate;
            var dataKoniec = dpDataZwrotu.SelectedDate;


            // Pobranie ogłoszeń wraz z pojazdami i informacją o liczbie sztuk
            var ogloszeniaZBazy = db.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .ThenInclude(p => p.IdSztukiNavigation)
                .Where(o => o.IdOgloszenia==carAd.IdOgloszenia)
                .ToList();

            // Pobranie wszystkich rezerwacji wraz z powiązanymi ogłoszeniami i pojazdami
            var rezerwacje = db.Rezerwacjes
                .Include(r => r.IdOgloszeniaNavigation)
                .ThenInclude(o => o.IdPojazduNavigation)
                .Where(r=>r.IdOgloszeniaNavigation.IdOgloszenia==carAd.IdOgloszenia)
                .ToList();

           

            // Przefiltruj ogłoszenia
            ogloszeniaZBazy = ogloszeniaZBazy.Where(ogloszenie =>
            {
                // Zlicz ile rezerwacji koliduje z danym terminem dla tego ogłoszenia
                var kolidujaceRezerwacje = rezerwacje.Where(r =>
                    r.IdOgloszenia == ogloszenie.IdOgloszenia &&
                    !CzyRezerwacjaMozliwa(dataPocz, dataKoniec, r.DataRozpoczeciaRezerwacji, r.DataZakonczeniaRezerwacji)
                ).Count();

                // Pobierz ilość sztuk pojazdu
                var iloscSztuk = ogloszenie.IdPojazduNavigation?.LiczbaSztuk ?? 0;

                // Zostaw ogłoszenie tylko jeśli są jeszcze wolne sztuki
                return kolidujaceRezerwacje < iloscSztuk;
            }).ToList();

            if (ogloszeniaZBazy.Count == 0)
            {
                MessageBox.Show("Pojazd nie jest dostępny w wybranym terminie.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                wynajemSzczegoly szczegoly = new wynajemSzczegoly(carAd, aktualnyUzytkownik, dataWypozyczenia, dataZwrotu)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Width = this.Width,
                    Height = this.Height,
                    Left = this.Left,
                    Top = this.Top,
                };
                this.Hide();
                szczegoly.ShowDialog();
                this.Close();
            }
            //#################
            
        }
    }
}
