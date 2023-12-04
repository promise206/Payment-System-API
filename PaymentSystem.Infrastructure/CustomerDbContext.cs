using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure
{
    public class CustomerDbContext : IdentityDbContext<Customer>
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> AppUser { get; set; }
        public DbSet<Merchant> Address { get; set; }
    }
}
