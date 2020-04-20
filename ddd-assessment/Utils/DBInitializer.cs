using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Utils
{
    public static class DBInitializer
    {
        public static string DbConnection { get;  set; }
        public static void InitDapperConnectionString(string connectionString)
        {
            DbConnection = connectionString;
        }
    }
}
