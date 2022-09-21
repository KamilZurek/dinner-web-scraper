using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerWebScraper
{
    class SimpleSQLDataWriter
    {
        public static void InsertDinnersIntoDB(List<Dinner> dinners)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DinnersManagerConnStr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach(var dinner in dinners)
                    {
                        string queryString = $@"INSERT INTO[dbo].[Dinners] ([Type], [Name] ,[Date]) 
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
            command.Parameters.Add(CreateQueryParameterForInsertDinners("@type", SqlDbType.NVarChar, dinner.Type.GetName()));
            command.Parameters.Add(CreateQueryParameterForInsertDinners("@name", SqlDbType.NVarChar, dinner.Name));
            command.Parameters.Add(CreateQueryParameterForInsertDinners("@date", SqlDbType.DateTime, dinner.Date));
        }

        private static SqlParameter CreateQueryParameterForInsertDinners(string name, SqlDbType type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                ParameterName = name,
                SqlDbType = type,
                Direction = direction,
                Value = value
            };
        }
    }
}
