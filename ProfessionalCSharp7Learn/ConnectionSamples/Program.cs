using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenConnection();
        }

        public static void OpenConnection()
        {
            string connectionString = @"server=.;" +
            "integrated security=true;database=Books";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            // Do something useful
            Console.WriteLine("Connection opened");
            connection.Close();
        }
    }
}
