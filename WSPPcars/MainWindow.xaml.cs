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


namespace WSPPcars;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnZarejestruj_Click(object sender, RoutedEventArgs e)
    {
        Rejestracja rejestracja = new Rejestracja();
        rejestracja.ShowDialog();
    }

    private void btnZaloguj_Click(object sender, RoutedEventArgs e)
    {
        Logowanie logowanie = new Logowanie();
        logowanie.ShowDialog();
    }
}