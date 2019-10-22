using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public void clkLogin(object sender, RoutedEventArgs e)
        {
            //open connection to database
            ConnectionManager cm = new ConnectionManager();

            //build query
            SqlCommand cnd = cm.Open("select * from Users where Username = @u and Password = @p");

            //add parameters
            SqlParameter username = cnd.Parameters.Add("@u", SqlDbType.VarChar);
            SqlParameter password = cnd.Parameters.Add("@p", SqlDbType.VarChar);
            username.Value = txtBoxUserName.Text;
            password.Value = txtBoxPassword.Text;

            //execute query
            SqlDataReader reader = cnd.ExecuteReader();

            //check if there was a matching record in DB
            if (reader.Read())
            {
                //checking case of username and password
                if (txtBoxUserName.Text.Equals(reader[0].ToString().TrimEnd()) && txtBoxPassword.Text.Equals(reader[1].ToString().TrimEnd()))
                {
                    //navigating to the main page
                    this.NavigationService.Navigate(new MainPage());
                }
                else
                {
                    //displays if the username or password had incorrect case
                    MessageBox.Show("Incorrect username or password");
                }
            }
            else
            {
                //displays if there was no matching record in DB
                MessageBox.Show("Incorrect username or password");
            }

            cm.Close();
        }
    }
}
