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
                if (p.Year > 1 && !academicHistory.Contains(p)) //don't want to include year 1 papers (because they don't have previous papers) or papers they've already taken
                {
                    //if prerequisites are met add to recommend papers list
                    if (p.Prerequisites.Count == 2)
                    {
                        //checking papers with two prerequisites
                        if (searchForPaper(p.Prerequisites[0], academicHistory) != null && searchForPaper(p.Prerequisites[1], academicHistory) != null)
                        {
                            recommendPapers.Add(p);
                        }
                    } //checking papers with one prerequisite
                    else if (p.Prerequisites.Count == 1)
                    {
                        if (searchForPaper(p.Prerequisites[0], academicHistory) != null)
                        {
                            recommendPapers.Add(p);
                        }
                    }
                    else //no prerequisites - like for I309 and I209
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
    }
}
