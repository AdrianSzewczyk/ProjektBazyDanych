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
using System.Windows.Navigation;
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
            WyswietlRezerwacje();
            WyswietlOgloszenia();
            WyswietlAuta();
            WyswietlUbezpieczenia();
            WyswietlDodatki();
            WyswietlUżytkownicy();
            WyswietlAutaSkrocone();
            WyswietlDodatkiSkrocone();
            WyswietlOgloszeniaSkrocone();
            WyswietlUbezpieczeniaSkrocone();
            WyswietlUżytkownicySkrocone();


        }
        // ------------------- Rezerwacje -------------------

        private void WyswietlRezerwacje()
        {
            listaRezerwacji.Items.Clear();
            var db = new DbWsppcarsContext();
            var rezerwacjeZbazy = db.Rezerwacjes.ToList();
            foreach ( var r in rezerwacjeZbazy)
            {
                listaRezerwacji.Items.Add(r);
            }
        }
        private void WyswietlOgloszeniaSkrocone()
        {
            listaOgloszenSkrocona.Items.Clear();

            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Ogloszenia
            .Include(o => o.IdPojazduNavigation)
            .ThenInclude(ps => ps.IdSztukiNavigation)

            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                listaOgloszenSkrocona.Items.Add(o);
            }

        }
        private void WyswietlDodatkiSkrocone()
        {
            listaDodatkowSkrocona.Items.Clear();

            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Dodatkis
            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                listaDodatkowSkrocona.Items.Add(o);
            }
        }
        private void WyswietlUbezpieczeniaSkrocone()
        {
            listaUbezpieczenSkrocona.Items.Clear();

            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Ubezpieczenia
            .Include(o => o.IdRodzajPakietuNavigation)
            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                listaUbezpieczenSkrocona.Items.Add(o);
            }
        }
        private void WyswietlUżytkownicySkrocone()
        {
            listaUzytkownikowSkrocona.Items.Clear();


            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Uzytkownicies
            .Include(o => o.IdRodzajKontaNavigation)
            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {

                listaUzytkownikowSkrocona.Items.Add(o);

            }

        }
        private void BtnDodajRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {
                var wybranaRezerwacje = listaRezerwacji.SelectedItem as Rezerwacje;
                if (wybranaRezerwacje != null)
                {
                    int stan;
                    if (comboStanRezerwacji.Text == "Opłacona") { stan = 1; } else { stan = 2; }
                        var rezerw = new Rezerwacje
                        {
                            IdOgloszenia = wybranaRezerwacje.IdOgloszenia,
                            IdUbezpieczenia = wybranaRezerwacje.IdUbezpieczenia,
                            IdUzytkownika = wybranaRezerwacje.IdUzytkownika,
                            IdStanRezerwacji = stan,
                            DataRozpoczeciaRezerwacji = datePoczatek.SelectedDate,
                            DataZakonczeniaRezerwacji = dateKoniec.SelectedDate,
                            Utworzona = DateTime.Now,
                        };

                    context.Rezerwacjes.Add(rezerw);
                    context.SaveChanges();
                }

            }


            WyswietlRezerwacje();
        }
        private void BtnUsunRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            var wybranaRezerwacje = listaRezerwacji.SelectedItem as Rezerwacje;

            if (wybranaRezerwacje == null)
            {
                MessageBox.Show("Proszę wybrać rezerwacje do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć tą rezerwację?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var rezerw = context.Rezerwacjes
                .FirstOrDefault(o => o.IdRezerwacji == wybranaRezerwacje.IdRezerwacji);

                if (rezerw == null)
                {
                    MessageBox.Show("Nie znaleziono rezerwacji w bazie.");
                    return;
                }

                context.Rezerwacjes.Remove(rezerw);
                context.SaveChanges();
            }


            WyswietlRezerwacje();
        }
        private void BtnEdytujRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            var wybranaRezerwacje = listaRezerwacji.SelectedItem as Rezerwacje;

            if (wybranaRezerwacje == null)
            {
                MessageBox.Show("Proszę wybrać rezerwację do edycji.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var rezerw = context.Rezerwacjes
                .FirstOrDefault(o => o.IdRezerwacji == wybranaRezerwacje.IdRezerwacji);

                if (rezerw == null)
                {
                    MessageBox.Show("Nie znaleziono ogłoszenia w bazie.");
                    return;
                }
                int stan;
                if (comboStanRezerwacji.Text == "Opłacona") { stan = 1; } else { stan = 2; }
                var ogl = listaOgloszenSkrocona.SelectedItem as Ogloszenium;
                var ubz = listaUbezpieczenSkrocona.SelectedItem as Ubezpieczenium;
                var uz = listaUzytkownikowSkrocona.SelectedItem as Uzytkownicy;
                rezerw.IdOgloszenia = ogl.IdOgloszenia;
                rezerw.IdUbezpieczenia = ubz.IdUbezpieczenia;
                rezerw.IdUzytkownika = uz.IdUzytkownika;
                rezerw.IdStanRezerwacji = stan;
                rezerw.DataRozpoczeciaRezerwacji = datePoczatek.SelectedDate;
                rezerw.DataZakonczeniaRezerwacji = dateKoniec.SelectedDate;
                context.SaveChanges();
            }

            WyswietlRezerwacje();
        }
        private void ListaRezerwacji_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var wybranaRezerwacje = listaRezerwacji.SelectedItem as Rezerwacje;

            if (wybranaRezerwacje != null)
            {
                using (var context = new DbWsppcarsContext())
                {
                 var rezerw = context.Rezerwacjes
                .FirstOrDefault(o => o.IdRezerwacji == wybranaRezerwacje.IdRezerwacji);

                    if (rezerw != null)
                    {
                        context.Entry(rezerw)
                            .Collection(r => r.DodatkiRezerwacjes)
                            .Query()
                            .Include(dr => dr.IdDodatkuNavigation)
                            .Load();

                        var dodatki_zebrane = rezerw.DodatkiRezerwacjes
                            .Select(dr => dr.IdDodatkuNavigation)
                            .Where(d => d != null)
                            .ToList();

                        foreach (var item in listaOgloszenSkrocona.Items)
                        {
                            if (item is Ogloszenium ogloszenie && ogloszenie.IdOgloszenia == rezerw.IdOgloszenia)
                            {
                                listaOgloszenSkrocona.SelectedItem = item;
                                listaOgloszenSkrocona.ScrollIntoView(item);
                                break;
                            }
                        }
                        foreach (var item in listaUbezpieczenSkrocona.Items)
                        {
                            if (item is Ubezpieczenium ubez && ubez.IdUbezpieczenia == rezerw.IdUbezpieczenia)
                            {
                                listaUbezpieczenSkrocona.SelectedItem = item;
                                listaUbezpieczenSkrocona.ScrollIntoView(item);
                                break;
                            }
                        }
                        foreach (var item in listaUzytkownikowSkrocona.Items)
                        {
                            if (item is Uzytkownicy uzy && uzy.IdUzytkownika == rezerw.IdUzytkownika)
                            {
                                listaUzytkownikowSkrocona.SelectedItem = item;
                                listaUzytkownikowSkrocona.ScrollIntoView(item);
                                break;
                            }
                        }

                        /*
                        foreach (var item in listaDodatkowSkrocona.Items)
                        {
                            if (item is Dodatki dod && dod.IdDodatku == rezerw.IdRezerwacji)
                            {
                                listaDodatkowSkrocona.SelectedItem = item;
                                listaDodatkowSkrocona.ScrollIntoView(item);
                                break;
                            }
                        }
                        */
                        //listaDodatkowSkrocona.Items.Clear();
                        listaDodatkowSkrocona.SelectedItems.Clear();
                        foreach (var item in listaDodatkowSkrocona.Items) 
                        { 
                        if(item is Dodatki dod &&  dodatki_zebrane.Find(o => o.IdDodatku == dod.IdDodatku) != null) 
                            {
                                listaDodatkowSkrocona.SelectedItems.Add(item);
                            }
                        }
                            comboStanRezerwacji.Text = rezerw.IdStanRezerwacji == 1 ? "Opłacona" : "Nieopłacona";
                        datePoczatek.SelectedDate = rezerw.DataRozpoczeciaRezerwacji;
                        dateKoniec.SelectedDate = rezerw.DataZakonczeniaRezerwacji;
                    }
                }

            }
        }
        //--------------------Ogłoszenia------------------------------
        private void WyswietlOgloszenia()
        {
            listaOgloszen.Items.Clear();

            var db = new DbWsppcarsContext();
            var ogloszeniaZBazy = db.Ogloszenia
            .Include(o => o.IdPojazduNavigation)
            .ThenInclude(ps => ps.IdSztukiNavigation)

            .ToList(); // <-- teraz dane są już w pamięci


            foreach (var o in ogloszeniaZBazy)
            {
                listaOgloszen.Items.Add(o);
            }

        }
        private void WyswietlAutaSkrocone()
        {
            listaAutSkrocona.Items.Clear();

            var db = new DbWsppcarsContext();
            var autaZBazy = db.Pojazds
            .Include(ps => ps.IdSztukiNavigation)
            .ToList();


            foreach (var a in autaZBazy)
            {
                listaAutSkrocona.Items.Add(a);
            }

        }

        private void BtnDodajOgloszenie_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new DbWsppcarsContext())
            {
                var wybraneAuto = listaAutSkrocona.SelectedItem as Pojazd;
                if (wybraneAuto != null)
                {
                    var ogloszenie = new Ogloszenium
                    {
                        IdPojazdu = wybraneAuto.IdPojazdu,
                        Dostepnosc = dostepnoscOgloszenie(),
                        DataDodania = DateTime.Now,
                        Kwota = Convert.ToDecimal(txtCena.Text)
                    };

                    context.Ogloszenia.Add(ogloszenie);
                    context.SaveChanges();
                }
                
            }
                WyswietlOgloszenia();
                WyswietlAutaSkrocone();
        }
        

        private void ListaOgloszen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybraneOgloszenie = listaOgloszen.SelectedItem as Ogloszenium;

            if (wybraneOgloszenie != null)
            {
                using (var context = new DbWsppcarsContext())
                {
                    var ogloszenie = context.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .FirstOrDefault(o => o.IdOgloszenia == wybraneOgloszenie.IdOgloszenia);

                    if (ogloszenie != null)
                    {
                        foreach (var item in listaAutSkrocona.Items)
                        {
                            if (item is Pojazd pojazd && pojazd.IdPojazdu == ogloszenie.IdPojazdu)
                            {
                                listaAutSkrocona.SelectedItem = item;
                                listaAutSkrocona.ScrollIntoView(item);
                                break;
                            }
                        }

                        comboDostepnoscOgloszenia.Text = ogloszenie.Dostepnosc == true ? "Dostępne" : "Niedostępne";
                        txtCena.Text = ogloszenie.Kwota.ToString();
                    }

                }

            }
        }

        private void BtnUsunOgloszenie_Click(object sender, RoutedEventArgs e)
        {
            var wybraneOgloszenie = listaOgloszen.SelectedItem as Ogloszenium;

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
                var ogloszenie = context.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .FirstOrDefault(o => o.IdOgloszenia == wybraneOgloszenie.IdOgloszenia);

                if (ogloszenie == null)
                {
                    MessageBox.Show("Nie znaleziono ogłoszenia w bazie.");
                    return;
                }

                context.Ogloszenia.Remove(ogloszenie);
                context.SaveChanges();
            }

            WyswietlOgloszenia(); // Odświeżenie listy
            WyswietlAutaSkrocone();
        }

        private void BtnEdytujOgloszenie_Click(object sender, RoutedEventArgs e)
        {
            var wybraneOgloszenie = listaOgloszen.SelectedItem as Ogloszenium;

            if (wybraneOgloszenie == null)
            {
                MessageBox.Show("Proszę wybrać ogłoszenie do edycji.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var ogloszenie = context.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .FirstOrDefault(o => o.IdOgloszenia == wybraneOgloszenie.IdOgloszenia);

                if (ogloszenie == null)
                {
                    MessageBox.Show("Nie znaleziono ogłoszenia w bazie.");
                    return;
                }
                var wybraneAuto = listaAutSkrocona.SelectedItem as Pojazd;
                if (wybraneAuto != null)
                {
                    ogloszenie.Kwota = Convert.ToDecimal(txtCena.Text);
                    ogloszenie.Dostepnosc = dostepnoscOgloszenie();
                    ogloszenie.IdPojazdu = wybraneAuto.IdPojazdu;
                    context.SaveChanges();

                }

                

            }
            WyswietlOgloszenia();
            WyswietlAutaSkrocone();
        }
        private bool dostepnoscOgloszenie()
        {
            bool dostepnosc = comboDostepnoscOgloszenia.Text switch
            {
                "Dostępne" => true,
                "Niedostępne" => false,
                _ => throw new Exception("Nieznana wartość dostępności.")
            };
            return dostepnosc;
        }
        private void ListaAutSkrocona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

            
        }

        // ------------------- Auta -------------------
        private int typPojazdu()
        {
            int typ = comboRodzajPojazdu.Text switch
            {
                "Spalinowy" => 1,
                "Hybrydowy" => 2,
                "Elektryczny" => 3,
                _ => throw new ArgumentException("Brak Typu Pojazdu")
            };
            return typ;
        }
        private bool skrzyniaBiegow()
        {
            bool skrzynia;

            switch (comboSkrzyniaBiegow.Text)
            {
                case "Manualna":
                    skrzynia = false;
                    break;
                case "Automatyczna":
                    skrzynia = true;
                    break;
                default:
                    throw new ArgumentException("Błędna Skrzynia Biegów");
            }

            return skrzynia;
        }
        private void WyswietlAuta()
        {
            listaAut.Items.Clear();

            var db = new DbWsppcarsContext();
            var autaZBazy = db.Pojazds
            .Include(ps => ps.IdSztukiNavigation)
            .ToList(); 


            foreach (var a in autaZBazy)
            {
                listaAut.Items.Add(a);
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
                    IdTypPojazdu = typPojazdu(),
                    PojemnoscSilnika = Convert.ToDecimal(txtSilnik.Text),
                    LiczbaDrzwi = Convert.ToInt16(txtDrzwi.Text),
                    LiczbaPasazerow = Convert.ToInt16(txtPasazerowie.Text),
                    AutomatycznaSkrzynia = skrzyniaBiegow(),
                    Zdjecie = txtZdjecie.Text,
                    Rocznik = DateOnly.FromDateTime(dpRocznik.SelectedDate.Value)
                };

                context.PojazdSztukas.Add(pojazd);
                context.SaveChanges();

                var poj = new Pojazd
                {
                    LiczbaSztuk = Convert.ToInt16(txtSztuki.Text),
                    IdSztuki = pojazd.IdPojazdSztuka
                };

                context.Pojazds.Add(poj);
                context.SaveChanges();

                
            }
            WyswietlAuta();
        }
        private void ListaAut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybraneAuto = listaAut.SelectedItem as Pojazd;

            if (wybraneAuto != null)
            {
                using (var context = new DbWsppcarsContext())
                {
                var auto = context.Pojazds
                .Include(o => o.IdSztukiNavigation)
                .ThenInclude(o=>o.IdTypPojazduNavigation)
                .FirstOrDefault(o => o.IdPojazdu == wybraneAuto.IdPojazdu);

                    if (auto != null)
                    {

                        txtMarka.Text = auto.IdSztukiNavigation.Marka;
                        txtModel.Text = auto.IdSztukiNavigation.Model;
                        txtDrzwi.Text = auto.IdSztukiNavigation.LiczbaDrzwi.ToString();
                        txtPasazerowie.Text = auto.IdSztukiNavigation.LiczbaPasazerow.ToString();
                        txtZdjecie.Text = auto.IdSztukiNavigation.Zdjecie;
                        dpRocznik.Text = auto.IdSztukiNavigation.Rocznik.ToString();
                        comboRodzajPojazdu.Text = auto.IdSztukiNavigation.IdTypPojazduNavigation.Typ;
                        comboSkrzyniaBiegow.Text = auto.IdSztukiNavigation.AutomatycznaSkrzynia==true?"Automatyczna":"Manualna";
                        txtSztuki.Text = auto.LiczbaSztuk.ToString();
                    }

                }

            }
        }
        private void BtnUsunAuto_Click(object sender, RoutedEventArgs e)
        {
            var wybraneAuto = listaAut.SelectedItem as Pojazd;

            if (wybraneAuto == null)
            {
                MessageBox.Show("Proszę wybrać pojazd do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć ten pojazd?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var auto = context.Pojazds
                .Include(p => p.IdSztukiNavigation)
                .ThenInclude(s => s.IdTypPojazduNavigation)
                .FirstOrDefault(o => o.IdPojazdu == wybraneAuto.IdPojazdu);

                if (auto == null)
                {
                    MessageBox.Show("Nie znaleziono pojazdu w bazie.");
                    return;
                }

                PojazdSztuka sztuka = null;

                if (auto != null)
                {
                    sztuka = context.PojazdSztukas.FirstOrDefault(s => s.IdPojazdSztuka == auto.IdSztuki);
                }

                
                if (auto != null) context.Pojazds.Remove(auto);
                if (sztuka != null) context.PojazdSztukas.Remove(sztuka);

                context.SaveChanges();
            }

            WyswietlAuta(); // Odświeżenie listy
            WyswietlAutaSkrocone();
        }
        private void BtnEdytujAuto_Click(object sender, RoutedEventArgs e)
        {
            var wybraneAuto = listaAut.SelectedItem as Pojazd;

            if (wybraneAuto == null)
            {
                MessageBox.Show("Proszę wybrać pojazd do edycji.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var auto = context.Pojazds
                .Include(p => p.IdSztukiNavigation)
                .ThenInclude(s => s.IdTypPojazduNavigation)
                .FirstOrDefault(o => o.IdPojazdu == wybraneAuto.IdPojazdu);

                if (auto == null)
                {
                    MessageBox.Show("Nie znaleziono pojazdu w bazie.");
                    return;
                }



                auto.LiczbaSztuk = Convert.ToInt16(txtSztuki.Text);
                auto.IdSztukiNavigation.Zdjecie = txtZdjecie.Text;
                auto.IdSztukiNavigation.Marka = txtMarka.Text;
                auto.IdSztukiNavigation.Model = txtModel.Text;
                auto.IdSztukiNavigation.IdTypPojazdu = typPojazdu();
                auto.IdSztukiNavigation.PojemnoscSilnika = Convert.ToDecimal(txtSilnik.Text);
                auto.IdSztukiNavigation.LiczbaDrzwi = Convert.ToInt16(txtDrzwi.Text);
                auto.IdSztukiNavigation.LiczbaPasazerow = Convert.ToInt16(txtPasazerowie.Text);
                auto.IdSztukiNavigation.AutomatycznaSkrzynia = skrzyniaBiegow();
                auto.IdSztukiNavigation.Rocznik = DateOnly.FromDateTime(dpRocznik.SelectedDate.Value);
                context.SaveChanges();

            }
            WyswietlAuta();
            WyswietlAutaSkrocone();
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
            

                listaUbezpieczen.Items.Add(o);

            }

        }
        private int rodzajPakietuUbezpieczenia()
        {
            int rodzaj = comboRodzajUbezpieczenia.Text switch
            {
                "Ekonomiczne" => 1,
                "Średnie" => 2,
                "Luksusowe" => 3,
                _ => throw new Exception("Nieznany rodzaj.")
            };
            return rodzaj;
        }
        private void BtnDodajUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenie = new Ubezpieczenium
                {
                    Nazwa = txtNazwaUbezp.Text,
                    NazwaUbezpieczalni = txtNazwaUbezpieczalni.Text,
                    Dostepnosc = dostepnoscUbezpieczenie(),
                    Kwota = Convert.ToDecimal(txtCenaUbezp.Text),
                    IdRodzajPakietu = rodzajPakietuUbezpieczenia()


                };

                context.Ubezpieczenia.Add(ubezpieczenie);
                context.SaveChanges();


            }
            WyswietlUbezpieczenia();
            WyswietlAutaSkrocone();
        }
        

        private void ListaUbezpieczen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybraneUbezpieczenie = listaUbezpieczen.SelectedItem as Ubezpieczenium;

            if (wybraneUbezpieczenie != null)
            {
                using (var context = new DbWsppcarsContext())
                {
                    var ubezpieczenie = context.Ubezpieczenia
                        .Include(o => o.IdRodzajPakietuNavigation).FirstOrDefault(o => o.IdUbezpieczenia == wybraneUbezpieczenie.IdUbezpieczenia);

                    if (ubezpieczenie != null)
                    {
                        txtNazwaUbezp.Text = ubezpieczenie.Nazwa;
                        txtNazwaUbezpieczalni.Text = ubezpieczenie.NazwaUbezpieczalni;
                        txtCenaUbezp.Text = ubezpieczenie.Kwota.ToString();
                        comboDostepnoscUbezpieczenia.Text = ubezpieczenie.Dostepnosc == true ? "Dostępne" : "Niedostępne";
                        comboRodzajUbezpieczenia.Text = ubezpieczenie.IdRodzajPakietuNavigation.Pakiet.ToString();
                    }

                }

            }
        }

        private void BtnUsunUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            var wybraneUbezpieczenie = listaUbezpieczen.SelectedItem as Ubezpieczenium;

            if (wybraneUbezpieczenie == null)
            {
                MessageBox.Show("Proszę wybrać ubezpieczenie do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć to ubezpieczenie?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenie = context.Ubezpieczenia
                        .Include(o => o.IdRodzajPakietuNavigation).FirstOrDefault(o => o.IdUbezpieczenia == wybraneUbezpieczenie.IdUbezpieczenia);

                if (ubezpieczenie == null)
                {
                    MessageBox.Show("Nie znaleziono ubezpieczenia w bazie.");
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
                MessageBox.Show("Proszę wybrać ubezpieczenie do edycji.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenie = context.Ubezpieczenia
                        .Include(o => o.IdRodzajPakietuNavigation).FirstOrDefault(o => o.IdUbezpieczenia == wybraneUbezpieczenie.IdUbezpieczenia);

                if (ubezpieczenie == null)
                {
                    MessageBox.Show("Nie znaleziono ubezpieczenia w bazie.");
                    return;
                }

                ubezpieczenie.Nazwa = txtNazwaUbezp.Text;
                ubezpieczenie.Kwota = Convert.ToDecimal(txtCenaUbezp.Text);
                ubezpieczenie.IdRodzajPakietu = rodzajPakietuUbezpieczenia();
                ubezpieczenie.Dostepnosc = dostepnoscUbezpieczenie();
                ubezpieczenie.NazwaUbezpieczalni =txtNazwaUbezpieczalni.Text ;

                context.SaveChanges();

            }
            WyswietlUbezpieczenia();
        }
        private bool dostepnoscUbezpieczenie()
        {
            bool dostepnosc = comboDostepnoscUbezpieczenia.Text switch
            {
                "Dostępne" => true,
                "Niedostępne" => false,
                _ => throw new Exception("Nieznana wartość dostępności.")
            };
            return dostepnosc;
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

                listaDodatkow.Items.Add(o);

            }

        }
        private bool dostepnoscDodatek()
        {
            bool dostepnosc = comboDostepnoscDodatku.Text switch
            {
                "Dostępne" => true,
                "Niedostępne" => false,
                _ => throw new Exception("Nieznana wartość dostępności.")
            };
            return dostepnosc;
        }
        private void BtnDodajDodatek_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {
                

                var dodatek = new Dodatki
                {
                    Nazwa = txtNazwaDodatku.Text,
                    LiczbaSztuk = txtLiczbaSztukDodatku.Text,
                    Dostepnosc = dostepnoscDodatek(),
                    Kwota = Convert.ToDecimal(txtCenaDodatku.Text) 
                };

                context.Dodatkis.Add(dodatek);
                context.SaveChanges();
            }

            WyswietlDodatki();
        }

        private void ListaDodatkow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybranyDodatek = listaDodatkow.SelectedItem as Dodatki;

            if (wybranyDodatek != null)
            {
                using (var context = new DbWsppcarsContext())
                {
                    var dodatek = context.Dodatkis.FirstOrDefault(o => o.IdDodatku == wybranyDodatek.IdDodatku);

                    if (dodatek != null)
                    {
                        txtNazwaDodatku.Text = dodatek.Nazwa;
                        txtLiczbaSztukDodatku.Text = dodatek.LiczbaSztuk;
                        txtCenaDodatku.Text = dodatek.Kwota.ToString();
                        comboDostepnoscDodatku.Text = dodatek.Dostepnosc == true ? "Dostępne" : "Niedostępne";
                    }

                }

            }
        }

        private void BtnUsunDodatek_Click(object sender, RoutedEventArgs e)
        {
            var wybranyDodatek = listaDodatkow.SelectedItem as Dodatki;

            if (wybranyDodatek == null)
            {
                MessageBox.Show("Proszę wybrać dodatek do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć ten dodatek?",
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
                MessageBox.Show("Proszę wybrać dodatek do edycji.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var dodatek = context.Dodatkis.FirstOrDefault(d => d.IdDodatku == wybranyDodatek.IdDodatku);

                if (dodatek == null)
                {
                    MessageBox.Show("Nie znaleziono dodatku w bazie.");
                    return;
                }

                dodatek.Nazwa = txtNazwaDodatku.Text;
                dodatek.Kwota = Convert.ToDecimal(txtCenaDodatku.Text);
                dodatek.LiczbaSztuk = txtCenaDodatku.Text;
                dodatek.Dostepnosc = dostepnoscDodatek();
                

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
               
                listaUzytkownikow.Items.Add(o);

            }

        }
        private int rodzajKonta()
        {
            int rodzaj = comboUprawnienia.Text switch
            {
                "Admin" => 2,
                "Gość" => 1,
                _ => throw new Exception("Nieznana wartość Uprawnień.")
            };
            return rodzaj;
        }
       
        private void BtnDodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DbWsppcarsContext())
            {

                var uzytkownik = new Uzytkownicy
                {
                    Imie = txtImie.Text,
                    Nazwisko = txtNazwisko.Text,
                    Login = txtLogin.Text,
                    Haslo = txtHaslo.Text, 
                    IdRodzajKonta = rodzajKonta(),
                    Utworzony = DateTime.Now


                };

                context.Uzytkownicies.Add(uzytkownik);
                context.SaveChanges();


            }
            WyswietlUżytkownicy();
        }


        private void ListaUzytkownikow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybranyUzytkownik = listaUzytkownikow.SelectedItem as Uzytkownicy;

            if (wybranyUzytkownik != null)
            {
                using (var context = new DbWsppcarsContext())
                {
                    var uzytkownik = context.Uzytkownicies.Include(o => o.IdRodzajKontaNavigation).FirstOrDefault(o => o.IdUzytkownika == wybranyUzytkownik.IdUzytkownika);

                    if (uzytkownik != null)
                    {
                        txtImie.Text = uzytkownik.Imie;
                        txtNazwisko.Text = uzytkownik.Nazwisko;
                        txtLogin.Text = uzytkownik.Login;
                        txtHaslo.Text = uzytkownik.Haslo;
                        comboUprawnienia.Text = uzytkownik.IdRodzajKonta == 1 ? "Gość" : "Admin";
                    }

                }

            }


        }
        


        private void BtnUsunUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            var wybranyUzytkownik = listaUzytkownikow.SelectedItem as Uzytkownicy;

            if (wybranyUzytkownik == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika do usunięcia.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Czy na pewno chcesz usunąć tego użytkownika?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult != MessageBoxResult.Yes)
                return;

            using (var context = new DbWsppcarsContext())
            {
                var uzytkownik = context.Uzytkownicies.Include(o => o.IdRodzajKontaNavigation).FirstOrDefault(o => o.IdUzytkownika == wybranyUzytkownik.IdUzytkownika);

                if (uzytkownik == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika w bazie.");
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
                MessageBox.Show("Proszę wybrać użytkownika do edycji.");
                return;
            }
            using (var context = new DbWsppcarsContext())
            {
                var uzytkownik = context.Uzytkownicies.Include(o => o.IdRodzajKontaNavigation).FirstOrDefault(u => u.IdUzytkownika == wybranyUzytkownik.IdUzytkownika);

                if (uzytkownik == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika w bazie.");
                    return;
                }

                uzytkownik.Imie = txtImie.Text;
                uzytkownik.Nazwisko = txtNazwisko.Text;
                uzytkownik.Login = txtLogin.Text;
                uzytkownik.Haslo = txtHaslo.Text;
                uzytkownik.IdRodzajKonta = rodzajKonta();

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