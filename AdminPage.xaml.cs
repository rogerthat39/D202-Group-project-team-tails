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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            getPapers();
        }

        ConnectionManager cm = new ConnectionManager();
        List<Paper> paperList = new List<Paper>();

        private void backToLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void getPapers()
        {
            SqlCommand cnd = cm.Open("select * from papers");

            //Create SQL adapter to link it to the data table
            SqlDataAdapter adp = new SqlDataAdapter(cnd);

            // Crearing a data table object
            DataTable dt = new DataTable();

            //using data table to fill the data grid
            adp.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            cm.Close();
        }

        //currently not working
        private void editCell(object sender, DataGridCellEditEndingEventArgs e)
        {
            //get the selected row
            var id1 = (DataRowView)datagrid.SelectedItem;

            var paper_code = "";
            var name = "";
            var compulsory = "";
            var year = "";
            var semesters = "";
            var prerequisites = "";
            var description = "";

            //get the selected column name
            var columnName = e.Column.Header.ToString();

            //initiate variables based on what was entered
            if(columnName == "name")
            {
                paper_code = id1.Row["paper_code"].ToString();
                name = ((TextBox)e.EditingElement).Text;
                compulsory = id1.Row["compulsory"].ToString();
                year = id1.Row["year"].ToString();
                semesters = id1.Row["semesters"].ToString();
                prerequisites = id1.Row["prerequisites"].ToString();
                description = id1.Row["description"].ToString();
                id1.Row.AcceptChanges();
            }
            else if(columnName == "compulsory")
            {
                paper_code = id1.Row["paper_code"].ToString();
                name = id1.Row["name"].ToString();
                compulsory = ((TextBox)e.EditingElement).Text;
                year = id1.Row["year"].ToString();
                semesters = id1.Row["semesters"].ToString();
                prerequisites = id1.Row["prerequisites"].ToString();
                description = id1.Row["description"].ToString();
                id1.Row.AcceptChanges();
            }
            else if(columnName == "year")
            {
                paper_code = id1.Row["paper_code"].ToString();
                name = id1.Row["name"].ToString();
                compulsory = id1.Row["compulsory"].ToString();
                year = ((TextBox)e.EditingElement).Text;
                semesters = id1.Row["semesters"].ToString();
                prerequisites = id1.Row["prerequisites"].ToString();
                description = id1.Row["description"].ToString();
                id1.Row.AcceptChanges();
            }
            else if(columnName == "semesters")
            {
                paper_code = id1.Row["paper_code"].ToString();
                name = id1.Row["name"].ToString();
                compulsory = id1.Row["compulsory"].ToString();
                year = id1.Row["year"].ToString();
                semesters = ((TextBox)e.EditingElement).Text;
                prerequisites = id1.Row["prerequisites"].ToString();
                description = id1.Row["description"].ToString();
                id1.Row.AcceptChanges();
            }
            else if(columnName == "prerequisites")
            {
                paper_code = id1.Row["paper_code"].ToString();
                name = id1.Row["name"].ToString();
                compulsory = id1.Row["compulsory"].ToString();
                year = id1.Row["year"].ToString();
                semesters = id1.Row["semesters"].ToString();
                prerequisites = ((TextBox)e.EditingElement).Text;
                description = id1.Row["description"].ToString();
                id1.Row.AcceptChanges();
            }
            else if(columnName == "description")
            {
                paper_code = id1.Row["paper_code"].ToString();
                name = id1.Row["name"].ToString();
                compulsory = id1.Row["compulsory"].ToString();
                year = id1.Row["year"].ToString();
                semesters = id1.Row["semesters"].ToString();
                prerequisites = id1.Row["prerequisites"].ToString();
                description = ((TextBox)e.EditingElement).Text;
                id1.Row.AcceptChanges();
            }

            //send update query to database
            UpdateTable(paper_code, name, compulsory, year, semesters, prerequisites, description);
        }

        private void UpdateTable(string paper_code, string name, string compulsory, string year, string semesters, string prerequisites, string description)
        {
            SqlCommand cmd = cm.Open("Update Papers set name=@name, compulsory=@compulsory, year=@year, semesters=@semesters, " +
                "prerequisites=@prerequisites, description=@description where paper_code= @paper_code");

            cmd.Parameters.AddWithValue("@paper_code", paper_code);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@compulsory", compulsory);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@semesters", semesters);
            cmd.Parameters.AddWithValue("@prerequisites", prerequisites);
            cmd.Parameters.AddWithValue("@description", description);

            cmd.ExecuteNonQuery();

            //closing database connection and reloading the datagrid
            cm.Close();
            getPapers();
        }
    }
}
