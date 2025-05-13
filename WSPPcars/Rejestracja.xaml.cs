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
    public partial class OknoRejestracja : Window
    {
        public OknoRejestracja()
        {
            InitializeComponent();
        }

        private void BtnZarejestruj_Click(object sender, RoutedEventArgs e)
        {
            if (txtHaslo.Password != txtPotwierdzHaslo.Password)
            {
                txtKomunikat.Text = "Hasła się nie zgadzają!";
                return;
            }
            txtKomunikat.Text = "Rejestracja zakończona pomyślnie!";
        }
    }
}
