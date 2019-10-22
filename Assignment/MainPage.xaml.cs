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
        public MainPage()
        {
            InitializeComponent();
            getPapers();
            getMajors();
            listbox.ItemsSource = paperList;
        }

        List<Paper> paperList = new List<Paper>();
        List<Major> majorList = new List<Major>();
        List<Paper> academicHistory = new List<Paper>();

        public void addPaperToHistory(object sender, RoutedEventArgs e)
        {
            //check if a paper is selected
            if (listbox.SelectedIndex >= 0)
            {
                //add the paper to the academic history list
                Paper x = (Paper)listbox.SelectedItem;
                academicHistory.Add(x);
            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        public void displayBySemester(object sender, RoutedEventArgs e)
        {
            listbox.ItemsSource = null;
            paperList.Sort();
            listbox.ItemsSource = paperList;
        }

        public void displayByMajor(object sender, RoutedEventArgs e)
        {
            //?
        }

        public void viewCourses(object sender, RoutedEventArgs e)
        {
            //clear list box and add the paper list
            listbox.ItemsSource = paperList;
        }

        public void getPapers()
        {
            ConnectionManager cm = new ConnectionManager();

            SqlCommand cnd = cm.Open("select * from papers");

            //execute query
            SqlDataReader reader = cnd.ExecuteReader();

            //loop through results
            while (reader.Read())
            {
                //extract information from current result
                string paperCode = reader[0].ToString();
                string name = reader[1].ToString();
                string desc = reader[2].ToString();
                string year = reader[4].ToString();
                string credits = reader[5].ToString();

                //convert "Y" or "N" to true or false
                bool compulsory = false;
                if (reader[3].ToString() == "Y")
                {
                    compulsory = true;
                }

                //convert comma-separated string to list
                List<int> semesters = new List<int>();
                string[] s = reader[6].ToString().Split(',');
                foreach (String i in s)
                {
                    semesters.Add(int.Parse(i));
                }

                //convert comma-separated string to list
                List<String> prerequisites = new List<String>();
                string[] x = reader[7].ToString().Split(',');
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

        public void getMajors()
        {
            //open connection to database
            ConnectionManager cm = new ConnectionManager();

            //build query
            SqlCommand cnd = cm.Open("select major_papers.major_id, name, paper_code from majors inner join major_papers on majors.major_id = major_papers.major_id");

            //execute query
            SqlDataReader reader = cnd.ExecuteReader();

            //loop through results
            while (reader.Read())
            {
                //check if major exists, otherwise create new Major object and add to list
                if(!doesMajorExist(reader[0].ToString(), reader[2].ToString())) {

                    List<Paper> papers = new List<Paper>();

                    //search for paper using paper code
                    foreach (Paper p in paperList)
                    {
                        if (p.Code == reader[2].ToString())
                        {
                            papers.Add(p);
                            break;
                        }
                    }
                    majorList.Add(new Major(reader[0].ToString(), reader[1].ToString(), papers));
                }
            }
            cm.Close();
        }

        public bool doesMajorExist(string major_id, string major_paper)
        {
            foreach (Major major in majorList)
            {
                if (major.MajorID == major_id)
                {
                    //add paper to existing major
                    Paper paper = null;
                    foreach (Paper p in paperList)
                    {
                        if (p.Code == major_paper)
                        {
                            paper = p;
                            break;
                        }
                    }
                    major.Papers.Add(paper);
                    return true;
                }
            }
            return false;
        }

        public void viewAcademicHistory(object sender, RoutedEventArgs e)
        {
            listbox.ItemsSource = academicHistory;
        }
    }
}
