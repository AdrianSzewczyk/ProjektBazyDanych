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
    public partial class wynajemSamochodu : Window
    {
        public wynajemSamochodu()
        {
            InitializeComponent();
        }

        private void BtnPrzejdz_Click(object sender, RoutedEventArgs e)
        {
            var dataWypozyczenia = dpDataWypozyczenia.SelectedDate;
            var dataZwrotu = dpDataZwrotu.SelectedDate;

            if (dataWypozyczenia == null || dataZwrotu == null)
            {
                MessageBox.Show("Proszę wybrać daty wypożyczenia i zwrotu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dataZwrotu <= dataWypozyczenia)
            {
                MessageBox.Show("Data zwrotu musi być późniejsza niż data wypożyczenia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                wynajemSzczegoly szczegoly = new wynajemSzczegoly();
                szczegoly.Owner = this;
                szczegoly.WindowStartupLocation = WindowStartupLocation.Manual;
                szczegoly.Width = this.Width;
                szczegoly.Height = this.Height;
                szczegoly.Left = this.Left;
                szczegoly.Top = this.Top;
                this.Hide();
                szczegoly.ShowDialog();
                this.Show();
            }          
        }
    }
}
