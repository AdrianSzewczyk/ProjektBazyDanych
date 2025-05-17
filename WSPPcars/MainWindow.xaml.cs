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
using System.Linq.Expressions;
using System.Windows.Automation;
using Microsoft.EntityFrameworkCore;

namespace WSPPCars
{

    /*public class CarAd
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Doors { get; set; }
        public double EngineCapacity { get; set; }
        public int MaxPeople { get; set; }
        public string TransmissionType { get; set; }
    }*/
    public class CarAdViewModel
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public decimal? Kwota { get; set; }
        public int LiczbaDrzwi { get; set; }
        public decimal? PojemnoscSilnika { get; set; }
        public int LiczbaPasazerow { get; set; }
        public string SkrzyniaBiegow { get; set; }
        public string ImagePath { get; set; }
    }

    public class MainWindowViewModel
    {
        //public ObservableCollection<CarAd> CarAds { get; set; }
        public ObservableCollection<PojazdSztuka> Pojazdy { get; set; }
        public ObservableCollection<Ogloszenium> Ogloszenia { get; set; }

        
            public ObservableCollection<CarAdViewModel> CarAds { get; set; }

            public MainWindowViewModel()
            {
                using var db = new DbWsppcarsContext();

                CarAds = new ObservableCollection<CarAdViewModel>(
                    db.Ogloszenia
                        .Include(o => o.IdPojazduNavigation)
                            .ThenInclude(ps => ps.IdSztukiNavigation).Where(o=>o.Dostepnosc==true)
                        .Select(o => new CarAdViewModel
                        {
                            Marka = o.IdPojazduNavigation.IdSztukiNavigation.Marka,
                            Model = o.IdPojazduNavigation.IdSztukiNavigation.Model,
                            Kwota = o.Kwota,
                            LiczbaDrzwi = o.IdPojazduNavigation.IdSztukiNavigation.LiczbaDrzwi ?? 0,
                            PojemnoscSilnika = o.IdPojazduNavigation.IdSztukiNavigation.PojemnoscSilnika ?? 0,
                            LiczbaPasazerow = o.IdPojazduNavigation.IdSztukiNavigation.LiczbaPasazerow ?? 0,
                            SkrzyniaBiegow = o.IdPojazduNavigation.IdSztukiNavigation.AutomatycznaSkrzynia == true ? "Automatyczna" : "Manualna",
                            //ImagePath = o.IdPojazduNavigation.IdSztukiNavigation.ImagePath
                        }).ToList());
            }
        


            //Pojazdy = new ObservableCollection<PojazdSztuka>

            /*new CarAd { Name = "Fiat Punto", Price = 35000, ImagePath = "./Images/toyota.jpg", Doors = 4, EngineCapacity = 1.8, MaxPeople = 5, TransmissionType = "Manualna" },
            new CarAd { Name = "BMW X6", Price = 400, ImagePath = "./Images/bmw.jpg", Doors = 5, EngineCapacity = 3.0, MaxPeople = 7, TransmissionType = "Automatyczna" },
            new CarAd { Name = "Lamborghini Aventador", Price = 10, ImagePath = "./Images/bmw1.jpg", Doors = 0, EngineCapacity = 0.0, MaxPeople = 1, TransmissionType = "Manualna" },
            new CarAd { Name = "Porshe Panamera", Price = 20, ImagePath = "./Images/porshe.jpg", Doors = 0, EngineCapacity = 1.8, MaxPeople = 5, TransmissionType = "Automatyczna" }
        };*/

            /*foreach (var ogloszenie in db.Ogloszenia)
            {
                //Ogloszenium  = new CarAd() { Name = pojazd.Marka, Price = 0, ImagePath = "./Images/bmw1.jpg", Doors = (int)pojazd.LiczbaDrzwi, EngineCapacity = (double)pojazd.PojemnoscSilnika, MaxPeople = (int)pojazd.LiczbaPasazerow, TransmissionType = (bool)pojazd.AutomatycznaSkrzynia? "Automatyczna" : "Manulana" };
                Ogloszenia.Add(ogloszenie);
            }*/
       // }
    }
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
                MainWindowViewModel ogloszenia = new MainWindowViewModel();
                DataContext = ogloszenia;
            AktualnyUzytkownik = new Uzytkownicy();
            AktualnyUzytkownik.Login = "Gosc";
            AktualnyUzytkownik.Imie = "Gosc";
            AktualnyUzytkownik.Nazwisko = "Gosc";
            AktualnyUzytkownik.Utworzony = DateTime.Now;
            AktualnyUzytkownik.Haslo = "";
        }

        private Uzytkownicy aktualnyUzytkownik;

        public Uzytkownicy AktualnyUzytkownik { get { return aktualnyUzytkownik; } set { aktualnyUzytkownik = value; } } 

        private void listboxOgloszenia_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is CarAdViewModel selectedCarAd)
            {
                    var detailsWindow = new OknoSzczegoly(selectedCarAd);
                    detailsWindow.ShowDialog();
            }
        }


        private void btnLogowanie_Click(object sender, RoutedEventArgs e)
        {
            OknoLogowanie oknoLogowanie = new OknoLogowanie();
            oknoLogowanie.ShowDialog();
        }

        private void btnRejestracja_Click(object sender, RoutedEventArgs e)
        {
            OknoRejestracja oknoRejestracja = new OknoRejestracja();
            oknoRejestracja.ShowDialog();
        }
        private void btnKonto_Click(object sender, RoutedEventArgs e)
        {
            MojeKonto oknoKonta = new MojeKonto(aktualnyUzytkownik);

            oknoKonta.ShowDialog();
        }

        private void btnWynajmij_Click(object sender, RoutedEventArgs e)
        {
            if(AktualnyUzytkownik.Login == "Gosc")
            {
                OknoLogowanie oknoLogowanie = new OknoLogowanie();
                oknoLogowanie.ShowDialog();
            }
            else
            {
                wynajemOgolny ogolny = new wynajemOgolny();
                ogolny.ShowDialog();
            }          
        }
    }
}
