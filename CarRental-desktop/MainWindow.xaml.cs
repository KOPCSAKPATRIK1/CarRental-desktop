using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBhelper dbhelper;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void torlesButton_Click(object sender, RoutedEventArgs e)
        {
            Car car = cars.SelectedItem as Car;
            if (car == null) 
            {
                MessageBox.Show("Törléshez előbb válasszon ki autót");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Biztos szeretné törölni a kiválasztott autó", "Biztos?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DeleteCar(car);
            }
        }

        private void DeleteCar(Car car)
        {
            try
            {
                if (dbhelper.UpdateBanned(car))
                {
                    MessageBox.Show("Sikeres torles");
                }
                else
                {
                    MessageBox.Show("Sikertelen torles");
                }
                cars.ItemsSource = dbhelper.ReadCars();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Hiba tortent a modositas soran");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dbhelper = new DBhelper();
                cars.ItemsSource = dbhelper.ReadCars();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}
