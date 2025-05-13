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
    public partial class MojeKonto : Window
    {
        public MojeKonto()
        {
            InitializeComponent();

            lblImieNazwisko.Text = "Łukasz Maliński";
            lblEmail.Text = "malina.lukas@onet.com";
            lblDataRejestracji.Text = DateTime.Now.AddMonths(-2).ToShortDateString();
        }

        private void BtnEdytuj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnZamowienia_Click(object sender, RoutedEventArgs e)
        {
            MojeZamowienia oknoZamowienia = new MojeZamowienia();
            oknoZamowienia.ShowDialog();
        }
    }
}