using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class OrderInfo
    {
        public int OrderInfoID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int numOfProduct { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
