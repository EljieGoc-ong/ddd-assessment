using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Utils
{
    public static class DapperStartup
    {
        public static IServiceCollection InitializeDapperConnectionString(this IServiceCollection services, string connectionString)
        {
            DBInitializer.InitDapperConnectionString(connectionString);

            return services;
        }
    }
}
