using Fundacion.Jala.DevInt.Practice2.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundacion.Jala.DevInt.Practice2.Store
{
    public class StockManager
    {
        private IList<object> _products;
        private IDictionary<Guid, int> _stockByProductId;
        public StockManager()
        {
            _products = new List<object>();
            _stockByProductId = new Dictionary<Guid, int>();
        }
        public void Add(object product, int quantity)
        {

        }

        public void Add(Guid productId, int quantity)
        {

        }

        public void Remove(Guid productId)
        {
            
        }

        public void Sell(Guid productId, int quantityToSell = 1)
        {
            
        }

        public IEnumerable<object> GetProductsBy(string type, string productFullName)
        {
            return new List<object>();
        }

        public IEnumerable<object> GetProductsBy(string productType)
        {
            return GetProductsBy(productType, string.Empty);
        }

        public int GetStockFor(Guid productId)
        {
            return 0;
        }

        public int GetStockFor(string productType)
        {
            return GetStockFor(productType, string.Empty);
        }

        public int GetStockFor(string productType, string productFullName)
        {
            return 0;
        }
    }
}
