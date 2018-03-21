using System;
using System.Collections.Generic;

namespace SuperMarket.Backoffice.Sales.Domain.Entities
{
    public class PriceTable
    {
        public static PriceTable Empty() => new PriceTable();
        public static PriceTable CreateTable(IDictionary<Guid, decimal> prices) => new PriceTable(prices);

        private readonly IDictionary<Guid, decimal> _table = new Dictionary<Guid, decimal>();

        private PriceTable(IDictionary<Guid, decimal> prices)
        {
            _table = prices;
        }

        private PriceTable()
        {
        }

        public void AddPrices(IDictionary<Guid, decimal> prices)
        {
            foreach (var price in prices)
            {
                AddPrice(price);
            }
        }

        public void AddPrice(KeyValuePair<Guid, decimal> price)
        {
            if (!_table.ContainsKey(price.Key))
                _table.Add(price.Key, price.Value);

            _table[price.Key] = price.Value;
        }

        public void RemovePrice(KeyValuePair<Guid, decimal> price)
        {
            if (_table.ContainsKey(price.Key))
                _table.Remove(price.Key);
        }

        public decimal GetPrice(Guid productId)
            => _table[productId];

        public bool ConstainsPrice(Guid productId)
            => _table.ContainsKey(productId);
    }
}
