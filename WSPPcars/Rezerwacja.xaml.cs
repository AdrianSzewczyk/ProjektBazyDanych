using System;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WSPPCars.Models;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;

namespace WSPPCars
{
    public partial class Rezerwacja : Window
    {
        private Ogloszenium carAd;
        private Ubezpieczenium ubezpieczenie;
        private Uzytkownicy aktualnyUzytkownik;
        private DateTime? dataWypozyczenia;
        private DateTime? dataZwrotu;
        private List<Dodatki> dodatki;
        public Rezerwacja()
        {
            InitializeComponent();
        }

        public Rezerwacja(Ogloszenium carAd, Ubezpieczenium u, Uzytkownicy aktualnyUzytkownik, DateTime? dataWypozyczenia, DateTime? dataZwrotu, List<Dodatki> d)
        {
            this.dataZwrotu = dataZwrotu;
            this.dataWypozyczenia = dataWypozyczenia;
            this.aktualnyUzytkownik = aktualnyUzytkownik;
            this.carAd = carAd;
            this.ubezpieczenie = u;
            this.dodatki = d;
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

        private async void btnPlatnosc_Click(object sender, RoutedEventArgs e)
        {
            if (chkRegulamin.IsChecked != true || chkRODO.IsChecked != true)
            {
                MessageBox.Show("Nie możesz przejść do płatności bez wymaganych zgód.", "Zgody wymagane", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            decimal? sumaDodatki = 0;
            //REZERWACJA DODANIE DO BAZY
            foreach(var d in dodatki)
            {
                sumaDodatki += (d.Kwota);
            }

            DateTime d1 = dataZwrotu.Value.Date;
            DateTime d2 = dataWypozyczenia.Value.Date;
            int IloscDni = Math.Abs((d1 - d2).Days);

            Decimal? kwota_do_zap;
            using (var context = new DbWsppcarsContext())
            {

                    var rezerwacja = new Rezerwacje
                    {
                        IdOgloszenia = carAd.IdOgloszenia,
                        DostepnoscPojazdu = true,//???
                        Utworzona = DateTime.Now,
                        IdUbezpieczenia = ubezpieczenie.IdUbezpieczenia,
                        IdStanRezerwacji = 2,//nieoplacona
                        IdUzytkownika = aktualnyUzytkownik.IdUzytkownika,
                        KwotaUbezpieczenia = ubezpieczenie.Kwota,
                        KwotaOgloszenia = carAd.Kwota,
                        DataRozpoczeciaRezerwacji = dataWypozyczenia,
                        DataZakonczeniaRezerwacji = dataZwrotu,
                        KwotaDodatku = sumaDodatki,
                        KwotaRezerwacji = ((carAd.Kwota * IloscDni) + ubezpieczenie.Kwota + sumaDodatki)

                    };

                    context.Rezerwacjes.Add(rezerwacja);
                    context.SaveChanges();

                foreach(var d in dodatki) {
                    var rezerwacjaDodatki = new DodatkiRezerwacje
                    {
                        IdRezerwacji = rezerwacja.IdRezerwacji,
                        IdDodatku = d.IdDodatku
                    };
                    context.DodatkiRezerwacjes.Add(rezerwacjaDodatki);
                    context.SaveChanges();
                }

                kwota_do_zap = rezerwacja.KwotaRezerwacji;
                kwota_do_zap.
            }

            //Koniec eksperymentow

            string podsumowanie = $"Imię: {txtImie.Text}\nNazwisko: {txtNazwisko.Text}\nEmail: {txtEmail.Text}";
            MessageBox.Show("Przechodzisz do płatności...\n\n" + podsumowanie, "Płatność", MessageBoxButton.OK, MessageBoxImage.Information);
            /*
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
            */
            //płatność
            try
            {
                // Ustaw klucz API testowy (SECRET key, tylko w trybie sandbox!)
                //tutaj klucz
                StripeConfiguration.ApiKey = "";

                // Tworzymy dane zamówienia
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "pln",
                        UnitAmountDecimal = kwota_do_zap*100, // pln
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Rezerwacja",
                        },
                    },
                    Quantity = 1,
                }
            },
                    Mode = "payment",
                    SuccessUrl = "https://wspp.com/success", // nie musi działać lokalnie
                    CancelUrl = "https://wspp.com/cancel"
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                // Otwórz przeglądarkę z formularzem Stripe Checkout
                Process.Start(new ProcessStartInfo
                {
                    FileName = session.Url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas tworzenia płatności: " + ex.Message);
            }
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