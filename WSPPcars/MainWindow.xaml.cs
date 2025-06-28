using System;
using System.IO;
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
using System.Collections.Generic;







namespace WSPPCars
{

   
    public partial class MainWindow : Window
    {

        public bool CzyRezerwacjaMozliwa(DateTime? poczatekRezerwacji, DateTime? koniecRezerwacji,
                                  DateTime? poczatekOkresu, DateTime? koniecOkresu)
        {
            bool pokrywaSie = poczatekRezerwacji < koniecOkresu && koniecRezerwacji > poczatekOkresu;
            return !pokrywaSie;
        }

        public void WyswietlenieListyOgloszen()
        {
            listboxOgloszenia.Items.Clear();
            using var db = new DbWsppcarsContext();

            var ogloszeniaZBazy = db.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .ThenInclude(ps => ps.IdSztukiNavigation)
                .Where(o => o.Dostepnosc == true)
                .ToList();



            foreach (var o in ogloszeniaZBazy)
            {
                listboxOgloszenia.Items.Add(o);
            }
        }
        private void dbInit()
        {
            using(var db = new DbWsppcarsContext())
            {
                bool emptyDatabase =
                    !db.Dodatkis.Any()&&
                    !db.Ogloszenia.Any()&&
                    !db.Pojazds.Any()&&
                    !db.Ubezpieczenia.Any()&&
                    !db.RodzajKonta.Any()&&
                    !db.RodzajPakietus.Any() &&
                    !db.TypPojazdus.Any();
                if (emptyDatabase)
                {
                   
                    //Uzupelniamy baze
                    //Typ Pojazdu
                    var typPoj = new List<TypPojazdu>
                    {
                        new TypPojazdu{Typ="Spalinowy"},
                        new TypPojazdu{Typ="Hybrydowy"},
                        new TypPojazdu{Typ="Elektryczny"}
                    };
                    db.TypPojazdus.AddRange(typPoj);
                    db.SaveChanges();
                    //Rodzaj Pakietu
                    var rodzPak = new List<RodzajPakietu>
                    {
                        new RodzajPakietu{Pakiet="Luksusowe" },
                        new RodzajPakietu{Pakiet="Średnie" },
                        new RodzajPakietu{Pakiet="Ekonomiczne" } 
                    };
                    db.RodzajPakietus.AddRange(rodzPak);
                    db.SaveChanges();
                    //Rodzaj Konta
                    var rodzKon = new List<RodzajKontum>
                    {
                        new RodzajKontum{Rodzaj="Użytkownik"},
                        new RodzajKontum{Rodzaj="Admin"},
                        new RodzajKontum{Rodzaj="NieZalogowany"}
                    };
                    db.RodzajKonta.AddRange(rodzKon);
                    db.SaveChanges();
                    //Dodatki
                    var dod = new List<Dodatki>
                    {
                        new Dodatki{Nazwa="Fotelik",LiczbaSztuk="3",Dostepnosc=true,Kwota=20},
                        new Dodatki{Nazwa="Bagażnik",LiczbaSztuk="10",Dostepnosc=true,Kwota=25},
                        new Dodatki{Nazwa="Pokrowce",LiczbaSztuk="12",Dostepnosc=true,Kwota=10}
                    };
                    db.Dodatkis.AddRange(dod);
                    db.SaveChanges();
                    var ube = new List<Ubezpieczenium>
                    {
                        new Ubezpieczenium{Nazwa="Prestige Shield",NazwaUbezpieczalni="Imperial Assurance",IdRodzajPakietu=1,Kwota=50,Dostepnosc=true },
                        new Ubezpieczenium{Nazwa="Diamond Care",NazwaUbezpieczalni="Pawlik Insurance",IdRodzajPakietu=2,Kwota=35,Dostepnosc=true }

                    };
                    db.Ubezpieczenia.AddRange(ube);
                    db.SaveChanges();
                    var pojSt = new List<PojazdSztuka>
                    {
                        new PojazdSztuka{Marka="Seat",Model="Ibiza",IdTypPojazdu=1,PojemnoscSilnika=1.4m,LiczbaDrzwi=5,LiczbaPasazerow=5,AutomatycznaSkrzynia=false,Rocznik=new DateOnly(2002,1,1),Zdjecie="https://cdn.proxyparts.com/vehicles/100385/8183964/large/01695e6a-5406-4792-9f26-40ff8d155188.jpg"},
                        new PojazdSztuka{Marka="Renault",Model="Clio",IdTypPojazdu=1,PojemnoscSilnika=1.2m,LiczbaDrzwi=3,LiczbaPasazerow=5,AutomatycznaSkrzynia=false,Rocznik=new DateOnly(2003,1,1),Zdjecie="https://thumbs.img-sprzedajemy.pl/1000x901c/01/20/b3/renault-clio-ii-2004-extreme-clio-lodz-316281772.jpg"},
                        new PojazdSztuka{Marka="Porche",Model="Carrera",IdTypPojazdu=1,PojemnoscSilnika=2.5m,LiczbaDrzwi=3,LiczbaPasazerow=2,AutomatycznaSkrzynia=true,Rocznik=new DateOnly(2022,1,1),Zdjecie="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSbDrz8MSIa859o4G2N6NyTsIuLoHJe48IUg&s"}
                    };
                    db.PojazdSztukas.AddRange(pojSt);
                    db.SaveChanges();
                    var poj = new List<Pojazd>
                    {
                        new Pojazd{IdSztuki=1,LiczbaSztuk=10},
                        new Pojazd{IdSztuki=2,LiczbaSztuk=15},
                        new Pojazd{IdSztuki=3,LiczbaSztuk=1 }
                    };
                    db.Pojazds.AddRange(poj);
                    db.SaveChanges();
                    var ogl = new List<Ogloszenium>
                    {
                        new Ogloszenium{IdPojazdu=1,Dostepnosc=true,DataDodania=DateTime.Now,Kwota=250},
                        new Ogloszenium{IdPojazdu=2,Dostepnosc=true,DataDodania=DateTime.Now,Kwota=200},
                        new Ogloszenium{IdPojazdu=3,Dostepnosc=true,DataDodania=DateTime.Now,Kwota=500}
                    };
                    db.Ogloszenia.AddRange(ogl);
                    db.SaveChanges();
                } 
            } 
        }
        public MainWindow()
        {
            InitializeComponent();
            //MainWindowViewModel ogloszenia = new MainWindowViewModel();
            //DataContext = ogloszenia;
            dbInit();
            AktualnyUzytkownik = new Uzytkownicy
            {
                Login = "Gosc",
                Imie = "Gosc",
                Nazwisko = "Gosc",
                Utworzony = DateTime.Now,
                Haslo = ""
            };
            //btnLogowanie.Visibility = Visibility.Visible;
            WyswietlenieListyOgloszen();
            btnAdminPanel.Visibility = Visibility.Collapsed;
            wyswietlenieModeli();
            wyswietlenieMarek();
            wyswietlenieRocznikow();
        }

        private Uzytkownicy aktualnyUzytkownik;

        public Uzytkownicy AktualnyUzytkownik { get { return aktualnyUzytkownik; } set { aktualnyUzytkownik = value; } } 

        

        private void listboxOgloszenia_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var wybraneOgloszenie = listboxOgloszenia.SelectedItem as Ogloszenium;
            if (wybraneOgloszenie!=null)
            {
                using (var context = new DbWsppcarsContext())
                {
                    var ogloszenie = context.Ogloszenia
                    .Include(o => o.IdPojazduNavigation)
                    .ThenInclude(o=>o.IdSztukiNavigation)
                    .FirstOrDefault(o => o.IdOgloszenia == wybraneOgloszenie.IdOgloszenia);
                    if (ogloszenie != null)
                    {
                        var oknoSzczegoly = new OknoSzczegoly(ogloszenie);
                        oknoSzczegoly.Owner = this;
                        oknoSzczegoly.WindowStartupLocation = WindowStartupLocation.Manual;
                        oknoSzczegoly.Width = this.Width;
                        oknoSzczegoly.Height = this.Height;
                        oknoSzczegoly.Left = this.Left;
                        oknoSzczegoly.Top = this.Top;
                        this.Hide();
                        oknoSzczegoly.ShowDialog();
                        this.Show();

                    }
                }
                    
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
            WyswietlenieListyOgloszen();
            

        }

        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            listboxOgloszenia.Items.Clear();
            using var db = new DbWsppcarsContext();

            var dataPocz = datePoczatek.SelectedDate;
            var dataKoniec = dateKoniec.SelectedDate;

            // Wczytaj ogłoszenia z pojazdami i danymi o sztukach
            var ogloszeniaZBazy = db.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .ThenInclude(p => p.IdSztukiNavigation)
                .Where(o => o.Dostepnosc == true)
                .ToList();

            // Filtrowanie po dostępności tylko jeśli obie daty są podane
            if (dataPocz != null && dataKoniec != null)
            {
                // Wczytaj rezerwacje
                var rezerwacje = db.Rezerwacjes
                    .Include(r => r.IdOgloszeniaNavigation)
                    .ThenInclude(o => o.IdPojazduNavigation)
                    .ToList();

                // Filtrowanie dostępnych sztuk
                ogloszeniaZBazy = ogloszeniaZBazy.Where(ogloszenie =>
                {
                    var kolidujace = rezerwacje.Where(r =>
                        r.IdOgloszenia == ogloszenie.IdOgloszenia &&
                        !CzyRezerwacjaMozliwa(dataPocz, dataKoniec, r.DataRozpoczeciaRezerwacji, r.DataZakonczeniaRezerwacji)
                    ).Count();

                    var iloscSztuk = ogloszenie.IdPojazduNavigation?.LiczbaSztuk ?? 0;

                    return kolidujace < iloscSztuk;
                }).ToList();
            }
            else if (dataPocz != null || dataKoniec != null)
            {
                MessageBox.Show("Wybierz obie daty lub żadnej.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Filtry dodatkowe: marka, model, typ, rocznik
            if (comboMarka.SelectedItem != null)
            {
                ogloszeniaZBazy = ogloszeniaZBazy
                    .Where(o => o.IdPojazduNavigation.IdSztukiNavigation.Marka == comboMarka.Text)
                    .ToList();
            }

            if (comboModel.SelectedItem != null)
            {
                ogloszeniaZBazy = ogloszeniaZBazy
                    .Where(o => o.IdPojazduNavigation.IdSztukiNavigation.Model == comboModel.Text)
                    .ToList();
            }

            if (comboRodzajPojazdu.SelectedItem != null)
            {
                ogloszeniaZBazy = ogloszeniaZBazy
                    .Where(o => o.IdPojazduNavigation.IdSztukiNavigation.IdTypPojazduNavigation.Typ == comboRodzajPojazdu.Text)
                    .ToList();
            }

            if (comboRocznik.SelectedItem != null)
            {
                string selectedText = comboRocznik.SelectedItem.ToString();
                if (DateOnly.TryParseExact(selectedText, "dd-MM-yyyy", out DateOnly wybranyRocznik))
                {
                    ogloszeniaZBazy = ogloszeniaZBazy
                        .Where(o => o.IdPojazduNavigation.IdSztukiNavigation.Rocznik == wybranyRocznik)
                        .ToList();
                }
            }

            // Wyświetlenie ogłoszeń
            foreach (var o in ogloszeniaZBazy)
            {
                listboxOgloszenia.Items.Add(o);
            }
        }

        private void btnResetuj_Click(object sender, RoutedEventArgs e)
        {
            datePoczatek.SelectedDate = null;
            dateKoniec.SelectedDate = null;
            comboMarka.SelectedItem = null;
            comboModel.SelectedItem = null;
            comboRocznik.SelectedItem = null;
            comboRodzajPojazdu.SelectedItem = null;
            WyswietlenieListyOgloszen();
            
        }

        private void comboUbezpieczenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void wyswietlenieMarek()
        {
            using (var context = new DbWsppcarsContext())
            {
                var ogloszenia = context.Ogloszenia.Include(o => o.IdPojazduNavigation)
                    .ThenInclude(o => o.IdSztukiNavigation)
                    .Where(o => o.Dostepnosc == true)
                    .Select(o => o.IdPojazduNavigation.IdSztukiNavigation.Marka).Distinct().ToList();
                comboMarka.ItemsSource = ogloszenia;
            }
        }
        private void wyswietlenieModeli()
        {
            using (var context = new DbWsppcarsContext())
            {
                var ogloszenia = context.Ogloszenia.Include(o => o.IdPojazduNavigation)
                    .ThenInclude(o => o.IdSztukiNavigation)
                    .Where(o => o.Dostepnosc == true)
                    .Select(o => o.IdPojazduNavigation.IdSztukiNavigation.Model).Distinct().ToList();
                comboModel.ItemsSource = ogloszenia;
            }

        }
        private void wyswietlenieRocznikow()
        {
            using (var context = new DbWsppcarsContext())
            {
                var ogloszenia = context.Ogloszenia.Include(o => o.IdPojazduNavigation)
                    .ThenInclude(o => o.IdSztukiNavigation)
                    .Where(o => o.Dostepnosc == true)
                    .Select(o => o.IdPojazduNavigation.IdSztukiNavigation.Rocznik).Distinct().ToList();
                comboRocznik.ItemsSource = ogloszenia;
            }
        }

        private void comboMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void comboRocznik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void comboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
