using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ApplicationUser : IdentityUser                                                                                    /*this class is created to add columns in userdetails table by inheriting from IdentityUser cause this IdentityUser cls is packege we con't make changes to add columns so by inherit this cls from Ideuse cls we can add some custom properties in this class*/
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
