using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchant.WebUI.Models
{
    public class Products
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public List<Products> ProductList { get; set; }

    }

    public class ProductList
    {
        private readonly List<Products> _items = new List<Products>();

        public List<Products> Items { get => _items; }
    }
}
