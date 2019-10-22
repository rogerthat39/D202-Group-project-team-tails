using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class ConnectionManager
    {
        //the connection string
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\D202-Group-project-team-tails-master\Assignment\bin\Debug\ProjectDB.mdf;Integrated Security=True;Connect Timeout=30");

        public SqlCommand Open(string query)
        {
            //opening the connection
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            return cmd;
        }

        public void Close()
        {
            con.Close();
        }
    }
}
