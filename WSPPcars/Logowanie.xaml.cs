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

    public partial class OknoLogowanie : Window
    {
        public OknoLogowanie()
        {
            InitializeComponent();
        }

        private void BtnZaloguj_Click(object sender, RoutedEventArgs e)
        {
            /*
            string login = txtNazwa.Text.Trim();
            string haslo = txtHaslo.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(haslo))
            {
                txtKomunikat.Text = "Uzupełnij wszystkie pola.";
                return;
            }

            if (login == "admin" && haslo == "1234")
            {
                txtKomunikat.Text = "Zalogowano pomyślnie.";

            }
            else
            {
                txtKomunikat.Text = "Nieprawidłowa nazwa użytkownika lub hasło.";
            }
            */
            using (var context = new DbWsppcarsContext())
            {
                string login;
                string haslo;
                if ((txtNazwa.Text == null || txtHaslo.Password == null))
                {                  
                    return;
                }
                else
                {
                    login = txtNazwa.Text;
                    haslo = txtHaslo.Password;
                }
                Uzytkownicy uzytkownik = null;
                foreach (var item in context.Uzytkownicies)
                {
                    if(item.Login.Equals(login))
                    {
                        uzytkownik = item;
                    }
                }
                if(uzytkownik == null)
                {
                    txtKomunikat.Text = "Nie ma takiego użytkownika. Zarejestruj się";
                }
                else
                {
                    if(haslo.Equals(uzytkownik.Haslo))
                    {
                        MainWindow mw = (MainWindow)Application.Current.MainWindow;
                        mw.AktualnyUzytkownik = uzytkownik;
                        txtKomunikat.Text = "Udało sie zalogować!!!";
                    }
                    else {
                        txtKomunikat.Text = "Niepoprawne hasło!!!";
                    }
                }
            }
            if(txtKomunikat.Text == "Udało sie zalogować!!!")
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.btnLogowanie.Visibility = Visibility.Collapsed;
                if (mw.AktualnyUzytkownik.IdRodzajKonta == 2)
                {
                    mw.btnAdminPanel.Visibility = Visibility.Visible;
                }
                    this.Close();             
            }
        }
    }
}