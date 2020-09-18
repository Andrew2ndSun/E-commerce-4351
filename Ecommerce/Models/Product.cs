using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public int productAmount { get; set; }
        public string productInfo { get; set; }
        public string active { get; set; }
        public string image { get; set; }
        public ICollection<OrderInfo> OrderInfos { get; set; }

      
    }
}
