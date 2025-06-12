using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
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
    public partial class panelAdmina : Window
    {
        public ObservableCollection<Auto> Auta { get; set; } = new();
        public ObservableCollection<Ubezpieczenie> Ubezpieczenia { get; set; } = new();
        public ObservableCollection<Dodatek> Dodatki { get; set; } = new();
        public ObservableCollection<Uzytkownik> Uzytkownicy { get; set; } = new();


        public panelAdmina()
        {
            InitializeComponent();
            /*listaAut.ItemsSource = Auta;
            listaUbezpieczen.ItemsSource = Ubezpieczenia;
            listaDodatkow.ItemsSource = Dodatki;
            listaUzytkownikow.ItemsSource = Uzytkownicy;*/
            WyswietlOgloszenia();
            WyswietlUbezpieczenia();
            WyswietlDodatki();
            WyswietlUżytkownicy();
        }

        // ------------------- Auta -------------------
       
        List<CarAdViewModel> CarAds { get; set; } 
        private void WyswietlOgloszenia()
        {
            listaAut.Items.Clear();

            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Ogloszenia
            .Include(o => o.IdPojazduNavigation)
            .ThenInclude(ps => ps.IdSztukiNavigation)

            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                string marka = o.IdPojazduNavigation.IdSztukiNavigation.Marka;
                string model = o.IdPojazduNavigation.IdSztukiNavigation.Model;
                decimal? kwota = o.Kwota;
                int? liczbaDrzwi = o.IdPojazduNavigation.IdSztukiNavigation.LiczbaDrzwi ;
                decimal? pojemnoscSilnika = o.IdPojazduNavigation.IdSztukiNavigation.PojemnoscSilnika;
                int? liczbaPasazerow = o.IdPojazduNavigation.IdSztukiNavigation.LiczbaPasazerow;
                string skrzyniaBiegow = o.IdPojazduNavigation.IdSztukiNavigation.AutomatycznaSkrzynia == true ? "Automatyczna" : "Manualna";
                listaAut.Items.Add(o);
            }



        

        }

        private void BtnDodajAuto_Click(object sender, RoutedEventArgs e)
        {


            using (var context = new DbWsppcarsContext())
            {
                var pojazd = new PojazdSztuka
                {
                    Marka = txtMarka.Text,
                    Model = txtModel.Text,
                    IdTypPojazdu = 1,
                    PojemnoscSilnika = Convert.ToDecimal(txtSilnik.Text),
                    LiczbaDrzwi = Convert.ToInt16(txtDrzwi.Text),
                    LiczbaPasazerow = Convert.ToInt16(txtPasazerowie.Text),
                    AutomatycznaSkrzynia = true,
                    Rocznik = new DateOnly(2022, 1, 1)
                };

                context.PojazdSztukas.Add(pojazd);
                context.SaveChanges(); 

                var poj = new Pojazd
                {
                    LiczbaSztuk = 1,
                    IdSztuki = pojazd.IdPojazdSztuka 
                };

                context.Pojazds.Add(poj);
                context.SaveChanges(); 

                var ogloszenie = new Ogloszenium
                {
                    IdPojazdu = poj.IdPojazdu, 
                    Dostepnosc = true,
                    DataDodania = DateTime.Now,
                    Kwota = Convert.ToDecimal(txtCena.Text)
                };

                context.Ogloszenia.Add(ogloszenie);
                context.SaveChanges();
            }
                WyswietlOgloszenia();
        }
        

        private void ListaAut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaAut.SelectedItem is Auto a)
            {
                txtMarka.Text = a.Marka;
                txtModel.Text = a.Model;
                txtCena.Text = a.Cena;
                txtDrzwi.Text = a.Drzwi;
                txtSilnik.Text = a.Silnik;
                txtPasazerowie.Text = a.Pasazerowie;
            }
        }

        private void BtnUsunAuto_Click(object sender, RoutedEventArgs e)
        {
            var wybraneOgloszenie = listaAut.SelectedItem as Ogloszenium;

            if (wybraneOgloszenie == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć to ogłoszenie?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var ogloszenie = context.Ogloszenia.FirstOrDefault(o => o.IdOgloszenia == wybraneOgloszenie.IdOgloszenia);

                if (ogloszenie == null)
                {
                    MessageBox.Show("Nie znaleziono ogłoszenia w bazie.");
                    return;
                }

                var pojazd = context.Pojazds.FirstOrDefault(p => p.IdPojazdu == ogloszenie.IdPojazdu);
                PojazdSztuka sztuka = null;

                if (pojazd != null)
                {
                    sztuka = context.PojazdSztukas.FirstOrDefault(s => s.IdPojazdSztuka == pojazd.IdSztuki);
                }

                context.Ogloszenia.Remove(ogloszenie);
                if (pojazd != null) context.Pojazds.Remove(pojazd);
                if (sztuka != null) context.PojazdSztukas.Remove(sztuka);

                context.SaveChanges();
            }

            WyswietlOgloszenia(); // Odświeżenie listy
        }

        private void BtnEdytujAuto_Click(object sender, RoutedEventArgs e)
        {
            var wybraneOgloszenie = listaAut.SelectedItem as Ogloszenium;

            if (wybraneOgloszenie == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var ogloszenie = context.Ogloszenia.FirstOrDefault(d => d.IdOgloszenia == wybraneOgloszenie.IdOgloszenia);

                if (ogloszenie == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika w bazie.");
                    return;
                }

                
                ogloszenie.Kwota = Convert.ToDecimal(txtCena.Text);
                ogloszenie.Dostepnosc = true;
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.Marka = txtMarka.Text;
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.Model = txtModel.Text;
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.IdTypPojazdu = 1;
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.PojemnoscSilnika = Convert.ToDecimal(txtSilnik.Text);
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.LiczbaDrzwi = Convert.ToInt16(txtDrzwi.Text);
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.LiczbaPasazerow = Convert.ToInt16(txtPasazerowie.Text);
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.AutomatycznaSkrzynia = true;
                ogloszenie.IdPojazduNavigation.IdSztukiNavigation.Rocznik = new DateOnly(2022, 1, 1);
                context.SaveChanges();

            }
            WyswietlOgloszenia();
        }

        // ------------------- Ubezpieczenia -------------------
        private void WyswietlUbezpieczenia()
        {
            listaUbezpieczen.Items.Clear();


            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Ubezpieczenia
            .Include(o => o.IdRodzajPakietuNavigation)
            .ToList(); // <-- teraz dane są już w pamięci

            
            foreach (var o in ogloszeniaZBazy)
            {
                string nazwa = o.Nazwa;
                string nazwaUbezpieczalni = o.NazwaUbezpieczalni;
                decimal? kwota = o.Kwota;
                bool? dostepnosc = o.Dostepnosc;
                string nazwaPakietu = o.IdRodzajPakietuNavigation.Pakiet;

                listaUbezpieczen.Items.Add(o);

            }



        }
        private void BtnDodajUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenie = new Ubezpieczenium
                {
                    Nazwa = txtNazwaUbezp.Text,
                    NazwaUbezpieczalni = "Szewczyk Corporation",
                    Dostepnosc = true,
                    Kwota = Convert.ToDecimal(txtCenaUbezp.Text),
                    IdRodzajPakietu = 3


                };

                context.Ubezpieczenia.Add(ubezpieczenie);
                context.SaveChanges();


            }
            WyswietlUbezpieczenia();
        }
        

        private void ListaUbezpieczen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaUbezpieczen.SelectedItem is Ubezpieczenie u)
            {
                txtNazwaUbezp.Text = u.Nazwa;
                txtCenaUbezp.Text = u.Cena;
            }
        }

        private void BtnUsunUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            var wybraneUbezpieczenie = listaUbezpieczen.SelectedItem as Ubezpieczenium;

            if (wybraneUbezpieczenie == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć to ogłoszenie?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenie = context.Ubezpieczenia.FirstOrDefault(o => o.IdUbezpieczenia == wybraneUbezpieczenie.IdUbezpieczenia);

                if (ubezpieczenie == null)
                {
                    MessageBox.Show("Nie znaleziono dodatku w bazie.");
                    return;
                }



                context.Ubezpieczenia.Remove(ubezpieczenie);
                context.SaveChanges();
            }

            WyswietlUbezpieczenia();
        }

        private void BtnEdytujUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            var wybraneUbezpieczenie = listaUbezpieczen.SelectedItem as Ubezpieczenium;

            if (wybraneUbezpieczenie == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenie = context.Ubezpieczenia.FirstOrDefault(d => d.IdUbezpieczenia == wybraneUbezpieczenie.IdUbezpieczenia);

                if (ubezpieczenie == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika w bazie.");
                    return;
                }

                ubezpieczenie.Nazwa = txtNazwaUbezp.Text;
                ubezpieczenie.Kwota = Convert.ToDecimal(txtCenaUbezp.Text);
                ubezpieczenie.IdRodzajPakietu = 2;
                ubezpieczenie.Dostepnosc = true;
                ubezpieczenie.NazwaUbezpieczalni ="Szewczyk Corporation" ;

                context.SaveChanges();

            }
            WyswietlUbezpieczenia();
        }

        // ------------------- Dodatki -------------------
        private void WyswietlDodatki()
        {
            listaDodatkow.Items.Clear();


            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Dodatkis
            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                string nazwa = o.Nazwa;
                string? liczbaSztuk = o.LiczbaSztuk;
                decimal? kwota = o.Kwota;
                bool? dostepnosc = o.Dostepnosc;
                

                listaDodatkow.Items.Add(o);

            }

        }
        private void BtnDodajDodatek_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {
                var dodatek = new Dodatki
                {
                    Nazwa = txtNazwaDodatku.Text,
                    LiczbaSztuk = "1",
                    Dostepnosc = true,
                    Kwota = Convert.ToDecimal(txtCenaDodatku.Text)


                };

                context.Dodatkis.Add(dodatek);
                context.SaveChanges();


            }
            WyswietlDodatki();
        }

        private void ListaDodatkow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaDodatkow.SelectedItem is Dodatek d)
            {
                txtNazwaDodatku.Text = d.Nazwa;
                txtCenaDodatku.Text = d.Cena;
            }
        }

        private void BtnUsunDodatek_Click(object sender, RoutedEventArgs e)
        {
            var wybranyDodatek = listaDodatkow.SelectedItem as Dodatki;

            if (wybranyDodatek == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć to ogłoszenie?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var dodatek = context.Dodatkis.FirstOrDefault(o => o.IdDodatku == wybranyDodatek.IdDodatku);

                if (dodatek == null)
                {
                    MessageBox.Show("Nie znaleziono dodatku w bazie.");
                    return;
                }

                

                context.Dodatkis.Remove(dodatek);
                context.SaveChanges();
            }

            WyswietlDodatki(); // Odświeżenie listy
        }

        private void BtnEdytujDodatek_Click(object sender, RoutedEventArgs e)
        {
            var wybranyDodatek = listaDodatkow.SelectedItem as Dodatki;

            if (wybranyDodatek == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var dodatek = context.Dodatkis.FirstOrDefault(d => d.IdDodatku == wybranyDodatek.IdDodatku);

                if (dodatek == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika w bazie.");
                    return;
                }

                dodatek.Nazwa = txtNazwaDodatku.Text;
                dodatek.Kwota = Convert.ToDecimal(txtCenaDodatku.Text);
                dodatek.LiczbaSztuk = "1";
                dodatek.Dostepnosc = true;
                

                context.SaveChanges();

            }
            WyswietlDodatki();
        }
        //---------------------Użytkownicy-----------------------------
        private void WyswietlUżytkownicy()
        {
            listaUzytkownikow.Items.Clear();


            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Uzytkownicies
            .Include(o=>o.IdRodzajKontaNavigation)
            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                string imie = o.Imie;
                string nazwisko = o.Nazwisko;
                string login = o.Login;
                string haslo = o.Haslo;
                DateTime? data = o.Utworzony;
                string rodzaj = o.IdRodzajKontaNavigation.Rodzaj;


                listaUzytkownikow.Items.Add(o);

            }

        }
        private void BtnDodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {
                var uzytkownik = new Uzytkownicy
                {
                    Imie = txtImieNazwisko.Text,
                    Nazwisko = txtImieNazwisko.Text,
                    Login = txtLogin.Text,
                    Haslo = txtHaslo.Text, 
                    IdRodzajKonta = 1,
                    Utworzony = DateTime.Now


                };

                context.Uzytkownicies.Add(uzytkownik);
                context.SaveChanges();


            }
            WyswietlUżytkownicy();
        }

        private void ListaUzytkownikow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaUzytkownikow.SelectedItem is Uzytkownik u)
            {
                txtImieNazwisko.Text = u.ImieNazwisko;
                txtLogin.Text = u.Login;
                txtHaslo.Text = u.Haslo;

                foreach (ComboBoxItem item in comboUprawnienia.Items)
                {
                    if (item.Content.ToString() == u.Uprawnienia)
                    {
                        comboUprawnienia.SelectedItem = item;
                        break;
                    }
                }
            }
        }


        private void BtnUsunUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            var wybranyUzytkownik = listaUzytkownikow.SelectedItem as Uzytkownicy;

            if (wybranyUzytkownik == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć to ogłoszenie?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var uzytkownik = context.Uzytkownicies.FirstOrDefault(o => o.IdUzytkownika == wybranyUzytkownik.IdUzytkownika);

                if (uzytkownik == null)
                {
                    MessageBox.Show("Nie znaleziono dodatku w bazie.");
                    return;
                }



                context.Uzytkownicies.Remove(uzytkownik);
                context.SaveChanges();
            }

            WyswietlUżytkownicy(); // Odświeżenie listy
        }

        private void BtnEdytujUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            var wybranyUzytkownik = listaUzytkownikow.SelectedItem as Uzytkownicy;

            if (wybranyUzytkownik == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do usunięcia.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var uzytkownik = context.Uzytkownicies.FirstOrDefault(u => u.IdUzytkownika == wybranyUzytkownik.IdUzytkownika);

                if (uzytkownik == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika w bazie.");
                    return;
                }

                uzytkownik.Imie = txtImieNazwisko.Text;
                uzytkownik.Nazwisko = txtImieNazwisko.Text;
                uzytkownik.Login = txtLogin.Text;
                uzytkownik.Haslo = txtHaslo.Text;
                uzytkownik.IdRodzajKonta = 1;

                context.SaveChanges();

            }
            WyswietlUżytkownicy();
            
                
                    
        }

    }


    // ---------- Proste klasy ----------
    public class Auto
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Cena { get; set; }
        public string Drzwi { get; set; }
        public string Silnik { get; set; }
        public string Pasazerowie { get; set; }
    }

    public class Ubezpieczenie
    {
        public string Nazwa { get; set; }
        public string Cena { get; set; }
    }

    public class Dodatek
    {
        public string Nazwa { get; set; }
        public string Cena { get; set; }
    }
    public class Uzytkownik
    {
        public string ImieNazwisko { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string Uprawnienia { get; set; }

        public override string ToString() => $"{Login} ({Uprawnienia})";
    }


}