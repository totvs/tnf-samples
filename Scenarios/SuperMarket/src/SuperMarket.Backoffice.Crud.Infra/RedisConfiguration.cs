using System;
using Microsoft.Extensions.Configuration;

namespace SuperMarket.Backoffice.Crud.Infra
{
    public class RedisConfiguration
    {
        public int DatabaseIndex { get; private set; }
        public string RedisConnectionString { get; private set; }

        public RedisConfiguration(IConfiguration configuration)
        {
            DatabaseIndex = Convert.ToInt32(configuration["DatabaseIndex"]);
            RedisConnectionString = configuration["RedisConnectionString"];

            if (string.IsNullOrWhiteSpace(RedisConnectionString))
                throw new NotSupportedException($"Invalid RedisConnectionString");
        }
    }
}
