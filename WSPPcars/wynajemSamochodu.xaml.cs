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

            lblKomunikat.Content = "Dane są poprawne. Przechodzenie dalej...";
            lblKomunikat.Foreground = Brushes.LightGreen;
            lblKomunikat.Visibility = Visibility.Visible;

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
    }
}
