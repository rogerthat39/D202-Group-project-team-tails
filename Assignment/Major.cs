using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Major
    {
        private string majorID;
        private string name;
        private List<Paper> papers;

        public string MajorID { get => majorID; set => majorID = value; }
        public string Name { get => name; set => name = value; }
        public List<Paper> Papers { get => papers; set => papers = value; }

        public Major(string id, string name, List<Paper> papers)
        {
            MajorID = id;
            Name = name;
            Papers = papers;
        }
    }
}
