using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerWebScraper.SQL
{
    class SimpleSQLDataWriter : SimpleSQLData
    {
        public static void InsertDinnersIntoDB(List<Dinner> dinners)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    foreach (var dinner in dinners)
                    {
                        string queryString = $@"INSERT INTO [dbo].[Dinners] ([Type], [Name] ,[Date]) 
                            VALUES (@type, @name, @date)";

                        var command = new SqlCommand(queryString, connection);
                        AddQueryParametersForInsertDinners(dinner, command);

                        int result = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void AddQueryParametersForInsertDinners(Dinner dinner, SqlCommand command)
        {
            command.Parameters.Add(CreateQueryParameter("@type", SqlDbType.NVarChar, dinner.Type.GetName()));
            command.Parameters.Add(CreateQueryParameter("@name", SqlDbType.NVarChar, dinner.Name));
            command.Parameters.Add(CreateQueryParameter("@date", SqlDbType.DateTime, dinner.Date));
        }

        public static void InsertDinnerDateIntoDB(DateTime date)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string queryString = $@"INSERT INTO [dbo].[DinnersStatus] ([DinnersDate], [Downloaded]) 
                            VALUES (@dinnerDate, @dateNow)";

                    var command = new SqlCommand(queryString, connection);
                    AddQueryParametersForInsertDinnerDate(date, command);

                    int result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void AddQueryParametersForInsertDinnerDate(DateTime date, SqlCommand command)
        {
            command.Parameters.Add(CreateQueryParameter("@dinnerDate", SqlDbType.DateTime, date));
            command.Parameters.Add(CreateQueryParameter("@dateNow", SqlDbType.DateTime, DateTime.Now));
        }
    }
}
