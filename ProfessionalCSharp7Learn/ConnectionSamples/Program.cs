using System;
using System.Collections.Generic;
using System.Data;
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
            "integrated security=true;database=AdventureWorks2014";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            // Do something useful
            Console.WriteLine("Connection opened");

            string sql = "SELECT TOP 10 FirstName FROM DimCustomer";

            var command = new SqlCommand(sql, connection);

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    string firstName = reader.GetString(0);
                    Console.WriteLine($"First name: {firstName}");
                }
            }

            connection.Close();
        }

        public static void CreateCommand()
        {

        }
    }
}
