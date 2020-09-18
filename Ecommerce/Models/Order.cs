using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System.Threading.Tasks;



namespace Ecommerce.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        [Display(Name = "Customer Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid Name")]
        public string CustomerInfo { get; set; }
        [Display(Name = "Street Address")]
        [RegularExpression(@"^[0-9]+\s+[a-zA-Z]+(\s*[a-zA-Z]*)*$", ErrorMessage ="Invalid Address")]
        public string StreetAddress { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid City Name")]
        public string City { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "Zip is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        [StringLength(5)]
        public string Zipcode { get; set; }
        public string PaymentMethod { get; set; }
        
        [Display(Name = "Name on Card")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid Name")]
        public string NameOnCard { get; set; }

        
        [Display(Name = "Card Number")]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Invalid Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "EXP Day")]
        [RegularExpression(@"[0-9]{2}/[0-9]{4}$", ErrorMessage = "Invalid date, please enter the date in the fomat mm/yyyy")]
        public string ExpDay { get; set; }

        [Display(Name = "Security Code")]
        [RegularExpression(@"[0-9]{3}$", ErrorMessage = "Invalid Security Code")]
        [StringLength(3)]
        public string SecCode { get; set; }
        

        public DateTime? DateAdded { get; set; }
        public int? Status { get; set; }
        public ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
