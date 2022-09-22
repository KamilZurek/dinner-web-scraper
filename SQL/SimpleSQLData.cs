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
    public class SimpleSQLData
    {
        protected static string ConnectionString = ConfigurationManager.ConnectionStrings["DinnersManagerConnStr"].ConnectionString;

        protected static SqlParameter CreateQueryParameter(string name, SqlDbType type, object value, ParameterDirection direction = ParameterDirection.Input)
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
