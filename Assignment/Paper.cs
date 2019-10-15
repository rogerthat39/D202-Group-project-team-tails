using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Paper
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
            return code + " " + name;
        }
    }
}
