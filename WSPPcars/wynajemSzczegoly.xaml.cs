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

    public partial class wynajemSzczegoly : Window
    {
        private Ogloszenium carAd;
        private Uzytkownicy aktualnyUzytkownik;
        private DateTime? dataWypozyczenia;
        private DateTime? dataZwrotu;
        public wynajemSzczegoly()
        {
            InitializeComponent();
            WyswietlDodatki();
            WyswietlUbezpieczenia();
        }

        public wynajemSzczegoly(Ogloszenium carAd, Uzytkownicy aktualnyUzytkownik, DateTime? dataWypozyczenia, DateTime? dataZwrotu)
        {
            this.aktualnyUzytkownik = aktualnyUzytkownik;
            this.carAd = carAd;
            InitializeComponent();
            WyswietlDodatki();
            WyswietlUbezpieczenia();
            this.dataWypozyczenia = dataWypozyczenia;
            this.dataZwrotu = dataZwrotu;
        }
        private void WyswietlDodatki()
        {
            listDodatki.Items.Clear();
            using (var context = new DbWsppcarsContext())
            {
                var ogloszenia = context.Dodatkis.ToList();
                foreach (var o in ogloszenia) {
                    listDodatki.Items.Add(o);
                }

            }
        }

        private void WyswietlUbezpieczenia()
        {
            comboUbezpieczenie.Items.Clear();
            using (var context = new DbWsppcarsContext())
            {
                var ubezpieczenia = context.Ubezpieczenia.Include(o => o.IdRodzajPakietuNavigation).ToList();
                foreach (var u in ubezpieczenia)
                {
                    comboUbezpieczenie.Items.Add(u);
                }

            }
        }

        private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            Ubezpieczenium u = (Ubezpieczenium)comboUbezpieczenie.SelectedItem;
            List<Dodatki> d = listDodatki.SelectedItems.Cast<Dodatki>().ToList();
            Rezerwacja rez = new Rezerwacja(carAd,u, aktualnyUzytkownik, dataWypozyczenia, dataZwrotu, d);
            rez.Owner = this;
            rez.WindowStartupLocation = WindowStartupLocation.Manual;
            rez.Width = this.Width;
            rez.Height = this.Height;
            rez.Left = this.Left;
            rez.Top = this.Top;
            this.Hide();
            rez.ShowDialog();
            this.Close();
        }

        private void listModeleAut_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}