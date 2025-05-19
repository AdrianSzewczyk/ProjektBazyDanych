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
using System.ComponentModel;

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
            AktualnyUzytkownik = new Uzytkownicy
            {
                Login = "Gosc",
                Imie = "Gosc",
                Nazwisko = "Gosc",
                Utworzony = DateTime.Now,
                Haslo = ""
            };
            //btnLogowanie.Visibility = Visibility.Visible;
            btnAdminPanel.Visibility = Visibility.Collapsed;
        }

        private Uzytkownicy aktualnyUzytkownik;

        public Uzytkownicy AktualnyUzytkownik { get { return aktualnyUzytkownik; } set { aktualnyUzytkownik = value; } } 

        

        private void listboxOgloszenia_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is CarAdViewModel selectedCarAd)
            {
                var detailsWindow = new OknoSzczegoly(selectedCarAd);
                detailsWindow.Owner = this;
                detailsWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                detailsWindow.Width = this.Width;
                detailsWindow.Height = this.Height;
                detailsWindow.Left = this.Left;
                detailsWindow.Top = this.Top;
                this.Hide();
                detailsWindow.ShowDialog();
                this.Show();
            }
        }


        private void btnLogowanie_Click(object sender, RoutedEventArgs e)
        {
            OknoLogowanie oknoLogowanie = new OknoLogowanie();
            oknoLogowanie.Owner = this;
            oknoLogowanie.WindowStartupLocation = WindowStartupLocation.Manual;
            oknoLogowanie.Width = this.Width;
            oknoLogowanie.Height = this.Height;
            oknoLogowanie.Left = this.Left;
            oknoLogowanie.Top = this.Top;
            this.Hide();
            oknoLogowanie.ShowDialog();
            this.Show();
        }

        private void btnRejestracja_Click(object sender, RoutedEventArgs e)
        {
            OknoRejestracja oknoRejestracja = new OknoRejestracja();
            oknoRejestracja.Owner = this;
            oknoRejestracja.WindowStartupLocation = WindowStartupLocation.Manual;
            oknoRejestracja.Width = this.Width;
            oknoRejestracja.Height = this.Height;
            oknoRejestracja.Left = this.Left;
            oknoRejestracja.Top = this.Top;
            this.Hide();
            oknoRejestracja.ShowDialog();
            this.Show();
        }
        private void btnKonto_Click(object sender, RoutedEventArgs e)
        {
            if(AktualnyUzytkownik.Login != "Gosc")
            {
                MojeKonto oknoKonta = new MojeKonto(aktualnyUzytkownik);
                oknoKonta.Owner = this;
                oknoKonta.WindowStartupLocation = WindowStartupLocation.Manual;
                oknoKonta.Width = this.Width;
                oknoKonta.Height = this.Height;
                oknoKonta.Left = this.Left;
                oknoKonta.Top = this.Top;
                this.Hide();
                oknoKonta.ShowDialog();
                this.Show();
            }
            else
            {
                OknoLogowanie oknoLogowanie = new OknoLogowanie();
                oknoLogowanie.Owner = this;
                oknoLogowanie.WindowStartupLocation = WindowStartupLocation.Manual;
                oknoLogowanie.Width = this.Width;
                oknoLogowanie.Height = this.Height;
                oknoLogowanie.Left = this.Left;
                oknoLogowanie.Top = this.Top;
                this.Hide();
                oknoLogowanie.ShowDialog();
                this.Show();
            }   
        }

        /*private void btnWynajmij_Click(object sender, RoutedEventArgs e)
        {
            if (AktualnyUzytkownik.Login == "Gosc")
            {
                OknoLogowanie oknoLogowanie = new OknoLogowanie();
                oknoLogowanie.ShowDialog();
            }
            else
            {
                wynajemOgolny ogolny = new wynajemOgolny();
                ogolny.ShowDialog();
            }
        }*/

        private void btnAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            panelAdmina adm = new panelAdmina();
            adm.Owner = this;
            adm.WindowStartupLocation = WindowStartupLocation.Manual;
            adm.Width = this.Width;
            adm.Height = this.Height;
            adm.Left = this.Left;
            adm.Top = this.Top;
            this.Hide();
            adm.ShowDialog();
            this.Show();
        }

        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnResetuj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
