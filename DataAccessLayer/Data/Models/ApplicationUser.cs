﻿using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Code { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }

    }
}
