using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class panelAdmina : Window
    {
        public ObservableCollection<Auto> Auta { get; set; } = new();
        public ObservableCollection<Ubezpieczenie> Ubezpieczenia { get; set; } = new();
        public ObservableCollection<Dodatek> Dodatki { get; set; } = new();
        public ObservableCollection<Uzytkownik> Uzytkownicy { get; set; } = new();


        public panelAdmina()
        {
            InitializeComponent();
            listaAut.ItemsSource = Auta;
            listaUbezpieczen.ItemsSource = Ubezpieczenia;
            listaDodatkow.ItemsSource = Dodatki;
            listaUzytkownikow.ItemsSource = Uzytkownicy;
        }

        // ------------------- Auta -------------------
        private void BtnDodajAuto_Click(object sender, RoutedEventArgs e)
        {
            Auta.Add(new Auto
            {
                Marka = txtMarka.Text,
                Model = txtModel.Text,
                Cena = txtCena.Text,
                Drzwi = txtDrzwi.Text,
                Silnik = txtSilnik.Text,
                Pasazerowie = txtPasazerowie.Text
            });
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
            if (listaAut.SelectedItem is Auto a)
                Auta.Remove(a);
        }

        private void BtnEdytujAuto_Click(object sender, RoutedEventArgs e)
        {
            if (listaAut.SelectedItem is Auto a)
            {
                a.Marka = txtMarka.Text;
                a.Model = txtModel.Text;
                a.Cena = txtCena.Text;
                a.Drzwi = txtDrzwi.Text;
                a.Silnik = txtSilnik.Text;
                a.Pasazerowie = txtPasazerowie.Text;
                listaAut.Items.Refresh();
            }
        }

        // ------------------- Ubezpieczenia -------------------
        private void BtnDodajUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            Ubezpieczenia.Add(new Ubezpieczenie
            {
                Nazwa = txtNazwaUbezp.Text,
                Cena = txtCenaUbezp.Text
            });
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
            if (listaUbezpieczen.SelectedItem is Ubezpieczenie u)
                Ubezpieczenia.Remove(u);
        }

        private void BtnEdytujUbezpieczenie_Click(object sender, RoutedEventArgs e)
        {
            if (listaUbezpieczen.SelectedItem is Ubezpieczenie u)
            {
                u.Nazwa = txtNazwaUbezp.Text;
                u.Cena = txtCenaUbezp.Text;
                listaUbezpieczen.Items.Refresh();
            }
        }

        // ------------------- Dodatki -------------------
        private void BtnDodajDodatek_Click(object sender, RoutedEventArgs e)
        {
            Dodatki.Add(new Dodatek
            {
                Nazwa = txtNazwaDodatku.Text,
                Cena = txtCenaDodatku.Text
            });
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
            if (listaDodatkow.SelectedItem is Dodatek d)
                Dodatki.Remove(d);
        }

        private void BtnEdytujDodatek_Click(object sender, RoutedEventArgs e)
        {
            if (listaDodatkow.SelectedItem is Dodatek d)
            {
                d.Nazwa = txtNazwaDodatku.Text;
                d.Cena = txtCenaDodatku.Text;
                listaDodatkow.Items.Refresh();
            }
        }
        private void BtnDodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            if (comboUprawnienia.SelectedItem is ComboBoxItem selected)
            {
                Uzytkownicy.Add(new Uzytkownik
                {
                    ImieNazwisko = txtImieNazwisko.Text,
                    Login = txtLogin.Text,
                    Haslo = txtHaslo.Text,
                    Uprawnienia = selected.Content.ToString()
                });
            }
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
            if (listaUzytkownikow.SelectedItem is Uzytkownik u)
                Uzytkownicy.Remove(u);
        }

        private void BtnEdytujUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            if (listaUzytkownikow.SelectedItem is Uzytkownik u && comboUprawnienia.SelectedItem is ComboBoxItem selected)
            {
                u.ImieNazwisko = txtImieNazwisko.Text;
                u.Login = txtLogin.Text;
                u.Haslo = txtHaslo.Text;
                u.Uprawnienia = selected.Content.ToString();
                listaUzytkownikow.Items.Refresh();
            }
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