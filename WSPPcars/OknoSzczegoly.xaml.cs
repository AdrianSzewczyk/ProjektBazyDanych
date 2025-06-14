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
    /// <summary>
    /// Interaction logic for OknoSzczegoly.xaml
    /// </summary>
    public partial class OknoSzczegoly : Window
    {
        private Ogloszenium carAd;
        public OknoSzczegoly()
        {
            InitializeComponent();
        }
        public OknoSzczegoly(Ogloszenium carAd)
        {
            InitializeComponent();
            DataContext = carAd;
            this.carAd = carAd;
        }

        private void btnWynajmij_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            if (mw.AktualnyUzytkownik.Login != "Gosc")
            {
                wynajemSamochodu wynajem = new wynajemSamochodu(carAd, mw.AktualnyUzytkownik);
                wynajem.Owner = this;
                wynajem.WindowStartupLocation = WindowStartupLocation.Manual;
                wynajem.Width = this.Width;
                wynajem.Height = this.Height;
                wynajem.Left = this.Left;
                wynajem.Top = this.Top;
                this.Hide();
                wynajem.ShowDialog();
                this.Close();
            }
            else
            {
                OknoLogowanie okno = new OknoLogowanie();
                okno.Owner = this;
                okno.WindowStartupLocation = WindowStartupLocation.Manual;
                okno.Width = this.Width;
                okno.Height = this.Height;
                okno.Left = this.Left;
                okno.Top = this.Top;
                this.Hide();
                okno.ShowDialog();
                this.Show();
            }
        }
    }
}
