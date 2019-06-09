using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentSolution.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Data
{
    public class PaymentDbContext : IdentityDbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
          //  options = options.ToString()
        }
        public DbSet<PaymentDetail> paymentDetails { get; set; }
    }
}
