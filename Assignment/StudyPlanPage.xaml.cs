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
    /// Interaction logic for StudyPlanPage.xaml
    /// </summary>
    public partial class StudyPlanPage : Page
    {
        public StudyPlanPage(List<Paper> ah, List<Paper> p)
        {
            InitializeComponent();
            academicHistory = ah;
            paperList = p;
        }

        List<Paper> academicHistory = new List<Paper>();
        List<Paper> paperList = new List<Paper>();

        /*List<Paper> academicHistory = new List<Paper>
            {
                new Paper("D111", "Database Fundamentals", null, 1, new List<int> {1}, null, true, 15),
                new Paper("T111", "Computer Hardware Fundamentals", null, 1, new List<int> {1}, null, true, 15),
                new Paper("I101", "Information System Fundamentals", null, 1, new List<int> {1}, null, true, 15)
            };*/

        /*List<Paper> paperList = new List<Paper>
            {
                new Paper("D211", "Database Development", null, 2, new List<int> {1}, new List<String> {"D111"}, true, 15),
                new Paper("T211", "Security", null, 2, new List<int> {1}, new List<String> {"T111"}, true, 15),
                new Paper("D111", "Database", null, 1, new List<int> {1}, null, true, 15),
                new Paper("I203", "Information System Fundamentals", null, 2, new List<int> {1}, new List<String> {"I101"}, false, 15),
                new Paper("I209", "Industry Placement", null, 2, new List<int> {2}, new List<String> {"I567"}, false, 15)
            };*/

        List<Paper> studyPlan = new List<Paper>();
        int totalCredits = 0;

        private void goToMainPage(object sender, RoutedEventArgs e)
        {
            //navigate back to main page
            this.NavigationService.Navigate(new MainPage(academicHistory));
        }

        private void recommendPapers(object sender, RoutedEventArgs e)
        {
            List<Paper> recommendPapers = new List<Paper>();

            foreach (Paper p in paperList)
            {
                if (p.Year > 1) //don't want to include year 1 papers in this because they don't have previous papers
                {
                    //if prerequisites are met, add to recommend papers list
                    if (p.Prerequisites.Count == 2)
                    {
                        if (searchForPaper(p.Prerequisites[0], academicHistory) != null && searchForPaper(p.Prerequisites[1], academicHistory) != null)
                        {
                            recommendPapers.Add(p);
                        }
                    }
                    else if (p.Prerequisites.Count == 1)
                    {
                        if (searchForPaper(p.Prerequisites[0], academicHistory) != null)
                        {
                            recommendPapers.Add(p);
                        }
                    }
                    else //if no prerequisites - like for I309 and I209
                    {
                        recommendPapers.Add(p);
                    }
                }
            }

            //add recommended papers to datagrid
            datagrid.ItemsSource = recommendPapers;
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

        private void createStudyPlan(object sender, RoutedEventArgs e)
        {
            //add compulsory papers
            //"D211, T211" should be added
            //d111 is a first year paper
            foreach(Paper p in paperList)
            {
                if(p.Compulsory && p.Year > 1)
                {
                    studyPlan.Add(p);
                    totalCredits += p.Credits;
                }
            }
            datagrid.ItemsSource = studyPlan;
        }

        private void testStudyPlan()
        {
            //check the study plan has x credits and right # of papers per semester?
            //check the study plan has all compulsory papers
            //check major (double major, unendorsed, or regular major)
        }

        private void submitStudyPlan(object sender, RoutedEventArgs e)
        {
            testStudyPlan();
        }

        /*
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.LoadCompleted += NavigationService_LoadCompleted;
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            paperList = (List<Paper>)e.ExtraData;
        }*/
    }
}
