using Microsoft.EntityFrameworkCore;
using Rest.Domain.App;
using Rest.Infra.Data.Map;

namespace Rest.Infra.Data.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ENV")))
            {
                contextOptionsBuilder.EnableDetailedErrors();
                contextOptionsBuilder.EnableSensitiveDataLogging();
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(DataBaseInformation.SCHEMA);

            builder.Entity<Customer>(new CustomerMap().Configure);
            builder.Entity<Address>(new AddressMap().Configure);
            builder.Entity<User>(new UserMap().Configure);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}