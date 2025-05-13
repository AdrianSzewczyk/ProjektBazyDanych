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

namespace WSPPCars
{
    /// <summary>
    /// Logika interakcji dla klasy OknoLogowanie.xaml
    /// </summary>
    public partial class OknoLogowanie : Window
    {
        public OknoLogowanie()
        {
            InitializeComponent();
        }

        private void BtnZaloguj_Click(object sender, RoutedEventArgs e)
        {
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
        }
    }
}