using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class EcommerceUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int level { get; set; }
        public string StreetAddress { get; internal set; }
        public string City { get; internal set; }
        public string State { get; internal set; }
        public string Zipcode { get; internal set; }
        //level=0 (admin)
        //level=2 (customer/user)
    }
}
