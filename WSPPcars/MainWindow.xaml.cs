using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WSPPCars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class CarAd
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Doors { get; set; }
        public double EngineCapacity { get; set; }
        public int MaxPeople { get; set; }
        public string TransmissionType { get; set; }
    }

    public class MainWindowViewModel
    {
        public ObservableCollection<CarAd> CarAds { get; set; }

        public MainWindowViewModel()
        {
            CarAds = new ObservableCollection<CarAd>
        {
            new CarAd { Name = "Fiat Punto", Price = 35000, ImagePath = "C:/samochody/toyota.jpg", Doors = 4, EngineCapacity = 1.8, MaxPeople = 5, TransmissionType = "Manualna" },
            new CarAd { Name = "BMW X6", Price = 400, ImagePath = "C:/samochody/bmw.jpg", Doors = 5, EngineCapacity = 3.0, MaxPeople = 7, TransmissionType = "Automatyczna" },
            new CarAd { Name = "Lamborghini Aventador", Price = 10, ImagePath = "C:/samochody/bmw1.jpg", Doors = 0, EngineCapacity = 0.0, MaxPeople = 1, TransmissionType = "Manualna" },
            new CarAd { Name = "Porshe Panamera", Price = 20, ImagePath = "C:/samochody/porshe.jpg", Doors = 0, EngineCapacity = 1.8, MaxPeople = 5, TransmissionType = "Automatyczna" }
        };
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel ogloszenia = new MainWindowViewModel();
            DataContext = ogloszenia;
        }


        private void listboxOgloszenia_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is CarAd selectedCarAd)
            {
                var detailsWindow = new OknoSzczegoly(selectedCarAd);
                detailsWindow.ShowDialog();
            }
        }


        private void btnLogowanie_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}