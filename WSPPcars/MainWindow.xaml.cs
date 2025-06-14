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
        public MainWindow()
        {
            InitializeComponent();
                //MainWindowViewModel ogloszenia = new MainWindowViewModel();
                //DataContext = ogloszenia;
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

            var ogloszeniaZBazy = db.Ogloszenia
                .Include(o => o.IdPojazduNavigation)
                .ThenInclude(ps => ps.IdSztukiNavigation)
                .Where(o => o.Dostepnosc == true)
                .ToList();

            var rezerwacje = db.Rezerwacjes
                .Include(r=>r.IdOgloszeniaNavigation)
                .ToList();

            List<Ogloszenium>? lista = new List<Ogloszenium>();

            DateTime? dataPocz = datePoczatek.SelectedDate;
            DateTime? dataKoniec = dateKoniec.SelectedDate;
            foreach (var r in rezerwacje)
            {
                if (r.IdOgloszeniaNavigation != null &&
                    !CzyRezerwacjaMozliwa(dataPocz, dataKoniec, r.DataRozpoczeciaRezerwacji, r.DataZakonczeniaRezerwacji))
                {
                    ogloszeniaZBazy.Remove(r.IdOgloszeniaNavigation);
                }
            }
            //ogloszeniaZBazy = ogloszeniaZBazy.Where(element => !lista.Contains(element)).ToList();

            foreach (var o in ogloszeniaZBazy)
            {
                listboxOgloszenia.Items.Add(o);
            }
        }

        private void btnResetuj_Click(object sender, RoutedEventArgs e)
        {
            datePoczatek.SelectedDate = null;
            dateKoniec.SelectedDate = null;
            
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
