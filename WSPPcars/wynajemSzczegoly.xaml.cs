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
        public wynajemSzczegoly()
        {
            InitializeComponent();
            WyswietlDodatki();
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

        private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {


            List<string> dostepneModele = new List<string>()
    {
        "Toyota Corolla",
        "Volkswagen Golf",
        "Ford Focus",
        "BMW 3 Series",
        "Audi A4"
    };

            listModeleAut.Items.Clear();
            foreach (var model in dostepneModele)
            {
                listModeleAut.Items.Add(model);
            }
        }

        private void listModeleAut_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Rezerwacja rez = new Rezerwacja();
            rez.Owner = this;
            rez.WindowStartupLocation = WindowStartupLocation.Manual;
            rez.Width = this.Width;
            rez.Height = this.Height;
            rez.Left = this.Left;
            rez.Top = this.Top;
            this.Hide();
            rez.ShowDialog();
            this.Show();
        }
    }
}