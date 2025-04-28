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
using WSPPCars.Models;

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
        public bool TransmissionType { get; set; }
    }

    public class MainWindowViewModel
    {
        public ObservableCollection<CarAd> CarAds { get; set; }
        public ObservableCollection<PojazdSztuka> Pojazdy { get; set; }

        public MainWindowViewModel()
        {
            CarAds = new ObservableCollection<CarAd>();
            var db = new DbWsppcarsContext();

            //Pojazdy = new ObservableCollection<PojazdSztuka>
            
                /*new CarAd { Name = "Fiat Punto", Price = 35000, ImagePath = "./Images/toyota.jpg", Doors = 4, EngineCapacity = 1.8, MaxPeople = 5, TransmissionType = "Manualna" },
                new CarAd { Name = "BMW X6", Price = 400, ImagePath = "./Images/bmw.jpg", Doors = 5, EngineCapacity = 3.0, MaxPeople = 7, TransmissionType = "Automatyczna" },
                new CarAd { Name = "Lamborghini Aventador", Price = 10, ImagePath = "./Images/bmw1.jpg", Doors = 0, EngineCapacity = 0.0, MaxPeople = 1, TransmissionType = "Manualna" },
                new CarAd { Name = "Porshe Panamera", Price = 20, ImagePath = "./Images/porshe.jpg", Doors = 0, EngineCapacity = 1.8, MaxPeople = 5, TransmissionType = "Automatyczna" }
            };*/
            
                foreach (var pojazd in db.PojazdSztukas)
                {
                    CarAd carAd = new CarAd() { Name = pojazd.Marka, Price = 0, ImagePath = "./Images/bmw1.jpg", Doors = (int)pojazd.LiczbaDrzwi, EngineCapacity = (double)pojazd.PojemnoscSilnika, MaxPeople = (int)pojazd.LiczbaPasazerow, TransmissionType = (bool)pojazd.AutomatycznaSkrzynia };
                    CarAds.Add(carAd);
                }

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
