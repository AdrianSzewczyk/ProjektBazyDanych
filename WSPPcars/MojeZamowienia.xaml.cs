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
    public partial class MojeZamowienia : Window
    {
        public MojeZamowienia()
        {
            InitializeComponent();

            List<Zamowienie> zamowienia = new List<Zamowienie>
            {
                new Zamowienie { NumerZamowienia = "Pierwsze", DataZamowienia = "2025-05-13", Status = "Wysłane" },
                new Zamowienie { NumerZamowienia = "Drugie", DataZamowienia = "2025-05-13", Status = "Oczekuje na płatność" },
                new Zamowienie { NumerZamowienia = "Trzecie", DataZamowienia = "2025-05-13", Status = "Dostarczone" }
            };
            ZamowieniaListView.ItemsSource = zamowienia;
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class Zamowienie
    {
        public string NumerZamowienia { get; set; }
        public string DataZamowienia { get; set; }
        public string Status { get; set; }
    }
}
