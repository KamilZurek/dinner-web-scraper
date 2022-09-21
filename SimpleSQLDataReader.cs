using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DinnerWebScraper
{
    class SimpleSQLDataReader
    {
        public static void GetDinnersFromDB()
        {
            string queryString = "SELECT * from dbo.Dinners Order By ID";
            string connectionString = ConfigurationManager.ConnectionStrings["DinnersManagerConnStr"].ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\n", "ID", "Typ", "Nazwa dania", "Data");

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader["ID"], reader["Type"], reader["Name"], reader["Date"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
