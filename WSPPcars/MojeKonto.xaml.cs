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
    public partial class MojeKonto : Window
    {
        public MojeKonto()
        {
            InitializeComponent();
        }

        public MojeKonto(Uzytkownicy u)
        {
            InitializeComponent();
            lblImieNazwisko.Text = u.Imie + " " + u.Nazwisko;
            lblLogin.Text = u.Login;    
            lblDataRejestracji.Text = u.Utworzony.ToString("dd-MM-yyyy HH:mm"); 

        }

        private void BtnEdytuj_Click(object sender, RoutedEventArgs e)
        {
            edycjaUzytkownika edycja = new edycjaUzytkownika();
            edycja.ShowDialog();
        }


        private void BtnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            Uzytkownicy AktualnyUzytkownik = new Uzytkownicy();
            AktualnyUzytkownik.Login = "Gosc";
            AktualnyUzytkownik.Imie = "Gosc";
            AktualnyUzytkownik.Nazwisko = "Gosc";
            AktualnyUzytkownik.Utworzony = DateTime.Now;
            AktualnyUzytkownik.Haslo = "";
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.AktualnyUzytkownik = AktualnyUzytkownik;
            lblImieNazwisko.Text = AktualnyUzytkownik.Imie + " " + AktualnyUzytkownik.Nazwisko;
            lblLogin.Text = AktualnyUzytkownik.Login;
            lblDataRejestracji.Text = AktualnyUzytkownik.Utworzony.ToString("dd-MM-yyyy HH:mm");
        }

        private void BtnZamowienia_Click(object sender, RoutedEventArgs e)
        {
            MojeZamowienia oknoZamowienia = new MojeZamowienia();
            oknoZamowienia.ShowDialog();
        }
    }
}