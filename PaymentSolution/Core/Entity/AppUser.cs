using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Core.Entity
{
    public class AppUser : IdentityUser
    {

        public string Fullname { get; set; }

      
    }
}
