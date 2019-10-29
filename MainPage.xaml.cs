using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(List<Paper> ah = null)
        {
            InitializeComponent();
            getPapers();
            getMajors();
            listbox.ItemsSource = paperList;
            if(ah != null)
            {
                academicHistory = ah;
            }
        }

        List<Paper> paperList = new List<Paper>();
        List<Major> majorList = new List<Major>();
        List<Paper> academicHistory = new List<Paper>();
        ConnectionManager cm = new ConnectionManager();

        private void addPaperToHistory(object sender, RoutedEventArgs e)
        {
            //check if a paper is selected
            if (listbox.SelectedIndex >= 0)
            {
                //check if paper is already in academic history
                Paper x = (Paper)listbox.SelectedItem;

                if (!academicHistory.Contains(x))
                {
                    //add the paper to the academic history list
                    academicHistory.Add(x);
                    MessageBox.Show(x.Code + " " + x.Name + " has been added.");
                }
                else
                {
                    MessageBox.Show("That paper is already in academic history");
                }
            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        private void displayBySemester(object sender, RoutedEventArgs e)
        {
            listbox.ItemsSource = null;
            paperList.Sort();
            listbox.ItemsSource = paperList;
        }

        private void displayByMajor(object sender, RoutedEventArgs e)
        {
            //?
        }

        private void goToStudyPage(object sender, RoutedEventArgs e)
        {
            //navigate to other page
            this.NavigationService.Navigate(new StudyPlanPage(academicHistory, paperList));
        }

        private void getPapers()
        {
            SqlCommand cnd = cm.Open("select * from papers");

            //execute query
            SqlDataReader reader = cnd.ExecuteReader();

            //loop through results
            while (reader.Read())
            {
                //extract information from current result
                string paperCode = reader[0].ToString();
                string name = reader[1].ToString();
                string desc = reader[7].ToString();
                string year = reader[3].ToString();
                string credits = reader[4].ToString();

                //convert "Y" or "N" to true or false
                bool compulsory = false;
                if (reader[2].ToString() == "Y")
                {
                    compulsory = true;
                }

                //convert comma-separated string to list
                List<int> semesters = new List<int>();
                string[] s = reader[5].ToString().Split(',');
                foreach (String i in s)
                {
                    semesters.Add(int.Parse(i));
                }

                //convert comma-separated string to list
                List<String> prerequisites = new List<String>();
                string[] x = reader[6].ToString().Split(',');
                foreach (String i in x)
                {
                    prerequisites.Add(i);
                }

                //create the Paper using information from result
                Paper p = new Paper(paperCode, name, desc, int.Parse(year), semesters, prerequisites, compulsory, int.Parse(credits));

                //add paper to list
                paperList.Add(p);
            }
            cm.Close();
        }

        private void getMajors()
        {
            //open connection to database
            SqlCommand cnd = cm.Open("select major_papers.major_id, name, paper_code from majors inner join major_papers on majors.major_id = major_papers.major_id");

            //execute query
            SqlDataReader reader = cnd.ExecuteReader();

            //loop through results
            while (reader.Read())
            {
                //check if major is in list already, otherwise create new Major object and add to list
                if(!doesMajorExist(reader[0].ToString(), reader[2].ToString())) {

                    List<Paper> papers = new List<Paper>();

                    //search for paper using paper code
                    papers.Add(searchForPaper(reader[2].ToString(), paperList));

                    majorList.Add(new Major(reader[0].ToString(), reader[1].ToString(), papers));
                }
            }
            cm.Close();
        }

        private bool doesMajorExist(string major_id, string major_paper)
        {
            foreach (Major major in majorList)
            {
                //if major does exist in majorList
                if (major.MajorID == major_id)
                {
                    //add paper to existing major
                    major.Papers.Add(searchForPaper(major_paper, paperList));
                    return true;
                }
            }
            return false;
        }

        //search the given list for the search term and return Paper that matches
        private Paper searchForPaper(string searchTerm, List<Paper> paperList)
        {
            foreach (Paper p in paperList)
            {
                if (p.Code == searchTerm)
                {
                    return p;
                }
            }
            return null;
        }

        private void viewAcademicHistory(object sender, RoutedEventArgs e)
        {
            listbox.ItemsSource = academicHistory;
        }
    }
}
