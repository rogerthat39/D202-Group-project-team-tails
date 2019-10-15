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

namespace Assignment
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

       
        public LoginPage()
        {
            InitializeComponent();
        }

        private void clkRegistration(object sender, RoutedEventArgs e)
        {
            //Navigating to the main page
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
