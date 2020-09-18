using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class UpdateUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid Name.Your first name cannot contain numbers")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid Name.Your last name cannot contain numbers")]

        public string LastName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        [RegularExpression(@"^[0-9]+\s+[a-zA-Z]+(\s*[a-zA-Z]*)*$", ErrorMessage = "Invalid Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid City Name")]

        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        [StringLength(5)]
        public string Zipcode { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
