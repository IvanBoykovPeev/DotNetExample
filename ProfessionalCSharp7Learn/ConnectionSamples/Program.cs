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
        static void Main()
        {
            OpenConnection();
        }

        public static void OpenConnection()
        {
            string connectionString = @"server=.;" +
            "integrated security=true;database=AdventureWorks2014";

            var connection = new SqlConnection(connectionString);

            //try
            //{
            //connection.Open();
            //Console.WriteLine("Connection opened");

            //ConnectWithSqlCommand(connection);

            //ConnectWithSqlCommandWithParameters(connection);




            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine(ex.Message); ;
            //}
            //finally
            //{
            //    connection.Close();

            //}

            ExecuteNonQuery(connection);

            if (connection.State == ConnectionState.Closed)
            {
                Console.WriteLine("Connection closed");
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection)
        {
            try
            {
                using (connection)
                {
                    string sql = "INSERT INTO [dbo].[DimSalesTerritory]" +
                        "([SalesTerritoryRegion], [SalesTerritoryCountry], [SalesTerritoryAlternateKey], [SalesTerritoryGroup]) " +
                        "VALUES (@SalesTerritoryRegion, @Country, @TerritoryAlternateKey, @TerritoryGroup)";
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("SalesTerritoryRegion", "Central");
                    command.Parameters.AddWithValue("Country", "Australia");
                    command.Parameters.AddWithValue("TerritoryAlternateKey", "55");
                    command.Parameters.AddWithValue("TerritoryGroup", "Europe");

                    connection.Open();
                    int records = command.ExecuteNonQuery();
                    Console.WriteLine($"{records} inserted");
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConnectWithSqlCommandWithParameters(SqlConnection connection)
        {
            string sql = "SELECT TOP 10 FirstName, LastName " +
                "FROM DimCustomer " +
                "WHERE FirstName = @FirstName AND LastName = @LastName";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("FirstName", SqlDbType.NVarChar);
            command.Parameters["FirstName"].Value = "Jon";
            command.Parameters.Add("LastName", SqlDbType.NVarChar);
            command.Parameters["LastName"].Value = "Yang";

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    string firstName = reader.GetString(0);
                    string lastName = reader.GetString(1);
                    Console.WriteLine($"First name: {firstName} Last name: {lastName}");
                }
            }
        }

        private static void ConnectWithSqlCommand(SqlConnection connection)
        {
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
        }
    }
}
