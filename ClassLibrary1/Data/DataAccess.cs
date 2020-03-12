using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace ClassLibrary.Data
{
    public static class DataAccess
    {
        private static string ConnectionValue(string name) // returns the connection string
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static List<T> GetData<T>(string sql, string skuId) // takes an sql string and a sku id and returns a list of items
        {
            var skuIdParam = new { skuId = skuId }; // creates a param for the query with the specified id
            using (var connection = new MySqlConnection(ConnectionValue("csvDB")))
            {
                return connection.Query<T>(sql, skuIdParam).ToList();
            }
        }
    }
}
