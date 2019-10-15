using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Paper: IComparable<Paper>
    {
        private string code;
        private string name;
        private string description;
        private int year;
        private List<int> semesters;
        private List<String> prerequisites;
        private bool compulsory;
        private int credits;

        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Year { get => year; set => year = value; }
        public List<int> Semesters { get => semesters; set => semesters = value; }
        public List<String> Prerequisites { get => prerequisites; set => prerequisites = value; }
        public bool Compulsory { get => compulsory; set => compulsory = value; }
        public int Credits { get => credits; set => credits = value; }

        public Paper(string code, string n, string d, int y, List<int> s, List<String> p, bool c, int i)
        {
            Code = code;
            Name = n;
            Description = d;
            Year = y;
            Semesters = s;
            Prerequisites = p;
            Compulsory = c;
            Credits = i;
        }

        public override string ToString()
        {
            if (this.Semesters.Count == 2)
            {
                return code + " " + name + " -- Semesters: " + Semesters[0] + " and " + Semesters[1];
            }
            else
            {
                return code + " " + name + " -- Semester: " + Semesters[0];
            }
        }

        public int CompareTo(Paper otherPaper)
        {
            //check the year and semester are not the same
            if(this.Year == otherPaper.Year && this.Semesters == otherPaper.Semesters)
            {
                return 0;
            }
            //display papers with lowest year first
            else if(this.Year > otherPaper.Year)
            {
                return 1;
            }
            else if (this.Year < otherPaper.Year)
            {
                return -1;
            }
            //if one paper has two semesters, it should be sorted below a paper with one semester
            else if(this.Semesters.Count == 2 && otherPaper.Semesters.Count == 1)
            {
                return 1;
            }
            else if (this.Semesters.Count == 1 && otherPaper.Semesters.Count == 2)
            {
                return -1;
            }
            //display semesters with lowest number first
            else if (this.Semesters[0] > otherPaper.Semesters[0])
            {
                return 1;
            }
            else if (this.Semesters[0] < otherPaper.Semesters[0])
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
