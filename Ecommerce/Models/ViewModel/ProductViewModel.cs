using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public int productAmount { get; set; }
        public string productInfo { get; set; }
        public string active { get; set; }
        public IFormFile image { get; set; }
    }
}
