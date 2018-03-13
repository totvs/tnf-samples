using System;

namespace RedisCache
{
    static class Constants
    {
        public static TimeSpan AbsoluteExpiration = TimeSpan.FromSeconds(5);
        public static string AllProductsCacheKey = "AllProducts";
    }
}
