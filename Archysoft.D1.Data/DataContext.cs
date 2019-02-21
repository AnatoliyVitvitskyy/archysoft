using System;
using Archysoft.D1.Data.Configurations;
using Archysoft.D1.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Archysoft.D1.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IConfiguration _configuration;

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var connection = environment == "Development" ? _configuration.GetConnectionString("DataContext") : Environment.GetEnvironmentVariable("ARCHYSOFT_D1_DATACONTEXT") ?? "";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserProfileConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
