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
            edycja.Owner = this;
            edycja.WindowStartupLocation = WindowStartupLocation.Manual;
            edycja.Width = this.Width;
            edycja.Height = this.Height;
            edycja.Left = this.Left;
            edycja.Top = this.Top;
            this.Hide();
            edycja.ShowDialog();
            this.Show();
        }


        private void BtnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            Uzytkownicy AktualnyUzytkownik = new Uzytkownicy();

            AktualnyUzytkownik.Login = "Gosc";
            AktualnyUzytkownik.Imie = "Gosc";
            AktualnyUzytkownik.Nazwisko = "Gosc";
            AktualnyUzytkownik.Utworzony = DateTime.Now;
            AktualnyUzytkownik.Haslo = "";
            mw.AktualnyUzytkownik = AktualnyUzytkownik;
            mw.btnLogowanie.IsEnabled = true;
            mw.btnLogowanie.Content = "Zaloguj się";
            mw.btnAdminPanel.Visibility = Visibility.Collapsed;
            lblImieNazwisko.Text = AktualnyUzytkownik.Imie + " " + AktualnyUzytkownik.Nazwisko;
            lblLogin.Text = AktualnyUzytkownik.Login;
            lblDataRejestracji.Text = AktualnyUzytkownik.Utworzony.ToString("dd-MM-yyyy HH:mm");
            this.Close();
        }

        private void BtnZamowienia_Click(object sender, RoutedEventArgs e)
        {
            MojeZamowienia oknoZamowienia = new MojeZamowienia();
            oknoZamowienia.Owner = this;
            oknoZamowienia.WindowStartupLocation = WindowStartupLocation.Manual;
            oknoZamowienia.Width = this.Width;
            oknoZamowienia.Height = this.Height;
            oknoZamowienia.Left = this.Left;
            oknoZamowienia.Top = this.Top;
            this.Hide();
            oknoZamowienia.ShowDialog();
            this.Show();
        }
    }
}