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
    public partial class OknoRejestracja : Window
    {

        public OknoRejestracja()
        {
            InitializeComponent();
        }
        
        private void BtnZarejestruj_Click(object sender, RoutedEventArgs e)
        { 
            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();
            string login = txtLogin.Text.Trim();
            string haslo = txtHaslo.Password;
            string potwierdzenieHasla = txtPotwierdzHaslo.Password;

            if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(login))
            {
                txtKomunikat.Text = "Uzupełnij wszystkie pola!";
                return;
            }

            if (haslo != potwierdzenieHasla)
            {
                txtKomunikat.Text = "Hasła się nie zgadzają!";
                return;
            }
                       
            using (var context = new DbWsppcarsContext())
            {
                bool istnieje = false;
                foreach(var l in context.Uzytkownicies) {
                    if(l.Login.Equals(login))
                    {
                        istnieje = true;
                    }
                }
                if (istnieje == false)
                {
                    var nowyUzytkownik = new Uzytkownicy
                    {
                        Imie = imie,
                        Nazwisko = nazwisko,
                        Login = login,
                        Haslo = haslo, 
                        Utworzony = DateTime.Now,
                        IdRodzajKonta = 1 
                    };

                    context.Uzytkownicies.Add(nowyUzytkownik);
                    context.SaveChanges();
                    txtKomunikat.Text = "Rejestracja zakończona pomyślnie!";
                }
                else
                {
                    txtKomunikat.Text = "Taki użytkownik już istnieje!";
                    txtImie.Clear();
                    txtNazwisko.Clear();
                    txtLogin.Clear();
                    txtHaslo.Clear();
                    txtPotwierdzHaslo.Clear();
                }
            }                    
        }    
    }
}
