using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DinnerWebScraper.SQL
{
    class SimpleSQLDataReader : SimpleSQLData
    {
        public static bool IsDinnerDateAlreadyInDB(DateTime date)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string queryString = "SELECT * from dbo.DinnersStatus WHERE DinnersDate = @date";
                var command = new SqlCommand(queryString, connection);
                AddQueryParametersForSelectDinnerStatus(date, command);

                try
                {
                    connection.Open();

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return true;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return false;
        }

        private static void AddQueryParametersForSelectDinnerStatus(object value, SqlCommand command)
        {
            command.Parameters.Add(CreateQueryParameter("@date", SqlDbType.DateTime, value));
        }

        public static void GetDinnersFromDB()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string queryString = "SELECT * from dbo.Dinners Order By ID";
                var command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    Console.WriteLine("Odczyt SQL:\n{0}\t{1}\t{2}\t{3}\n", "ID", "Typ", "Nazwa dania", "Data");

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

                Console.WriteLine();
            }
        }
    }
}
