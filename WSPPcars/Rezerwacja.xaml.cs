using System;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WSPPCars
{
    public partial class Rezerwacja : Window
    {
        public Rezerwacja()
        {
            InitializeComponent();
        }

        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (tb.Text == "Imię..." || tb.Text == "Nazwisko..." || tb.Text == "Email..."))
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == txtImie) tb.Text = "Imię...";
                if (tb == txtNazwisko) tb.Text = "Nazwisko...";
                if (tb == txtEmail) tb.Text = "Email...";
                tb.Foreground = Brushes.Gray;
            }
        }

        private void btnPotwierdz_Click(object sender, RoutedEventArgs e)
        {
            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(imie) || imie == "Imię..." ||
                string.IsNullOrEmpty(nazwisko) || nazwisko == "Nazwisko..." ||
                string.IsNullOrEmpty(email) || email == "Email...")
            {
                MessageBox.Show("Wypełnij wszystkie pola: imię, nazwisko i e-mail.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Podany e-mail jest nieprawidłowy.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (chkRegulamin.IsChecked != true || chkRODO.IsChecked != true)
            {
                MessageBox.Show("Aby kontynuować, musisz zaakceptować regulamin i RODO.", "Zgody wymagane", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Dane zostały poprawnie potwierdzone.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnPlatnosc_Click(object sender, RoutedEventArgs e)
        {
            if (chkRegulamin.IsChecked != true || chkRODO.IsChecked != true)
            {
                MessageBox.Show("Nie możesz przejść do płatności bez wymaganych zgód.", "Zgody wymagane", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string podsumowanie = $"Imię: {txtImie.Text}\nNazwisko: {txtNazwisko.Text}\nEmail: {txtEmail.Text}";
            MessageBox.Show("Przechodzisz do płatności...\n\n" + podsumowanie, "Płatność", MessageBoxButton.OK, MessageBoxImage.Information);

            OknoPlatnosc pla = new OknoPlatnosc();
            pla.Owner = this;
            pla.WindowStartupLocation = WindowStartupLocation.Manual;
            pla.Width = this.Width;
            pla.Height = this.Height;
            pla.Left = this.Left;
            pla.Top = this.Top;
            this.Hide();
            pla.ShowDialog();
            this.Show();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void BtnZobaczRegulamin_Click(object sender, RoutedEventArgs e)
        {
            string regulamin = @"
REGULAMIN WYPOŻYCZALNI SAMOCHODÓW WSPPCars

§1 Postanowienia ogólne
1. Niniejszy regulamin określa zasady korzystania z usług wypożyczalni samochodów WSPPCars.
2. Klient zobowiązany jest do zapoznania się z regulaminem przed dokonaniem rezerwacji.
3. Wypożyczenie samochodu jest równoznaczne z akceptacją niniejszego regulaminu.

§2 Warunki wynajmu
1. Najemcą może być osoba posiadająca:
   - ważne prawo jazdy od co najmniej 12 miesięcy,
   - ukończone 21 lat,
   - ważny dokument tożsamości.
2. Samochód wydawany jest w stanie sprawnym technicznie, czystym i z pełnym bakiem paliwa.

§3 Obowiązki najemcy
1. Najemca zobowiązuje się:
   - używać pojazdu zgodnie z jego przeznaczeniem,
   - nie udostępniać pojazdu osobom trzecim bez zgody wypożyczalni,
   - przestrzegać przepisów ruchu drogowego,
   - zwrócić pojazd w ustalonym terminie, w takim samym stanie i z pełnym bakiem.

§4 Ubezpieczenie i odpowiedzialność
1. Wszystkie pojazdy są objęte ubezpieczeniem OC, AC i NNW.
2. Najemca ponosi odpowiedzialność za:
   - szkody powstałe z jego winy,
   - szkody nieobjęte ubezpieczeniem (np. jazda po alkoholu, przekroczenie prędkości),
   - brak zgłoszenia kolizji na policję.
3. Istnieje możliwość wykupienia dodatkowego ubezpieczenia rozszerzonego lub premium.

§5 Zwrot pojazdu
1. Samochód należy zwrócić w ustalonym miejscu i terminie.
2. Za opóźnienie w zwrocie pojazdu bez wcześniejszego kontaktu naliczana jest kara umowna w wysokości 100 zł za każdą rozpoczętą godzinę.
3. Zwrot brudnego pojazdu skutkuje opłatą za czyszczenie (min. 50 zł).

§6 Dane osobowe
1. Dane osobowe najemcy są przetwarzane zgodnie z obowiązującymi przepisami RODO.
2. Administratorem danych jest WSPPCars Sp. z o.o., ul. Przykładowa 1, 00-000 Warszawa.
3. Dane są wykorzystywane wyłącznie w celu realizacji usługi i nie będą udostępniane osobom trzecim bez zgody klienta.

§7 Postanowienia końcowe
1. Wszelkie spory będą rozstrzygane przez właściwy sąd według siedziby wypożyczalni.
2. Regulamin wchodzi w życie z dniem 1 stycznia 2025 r.
3. WSPPCars zastrzega sobie prawo do wprowadzenia zmian w regulaminie.
";

            MessageBox.Show(regulamin, "Regulamin", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}