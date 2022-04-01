using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public string TenantId { get; set; }
        private readonly ITenantService _tenantService;
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService): base(options)
        {
            _tenantService = tenantService;
            TenantId = _tenantService.GetTenant()?.TID;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectionString = _tenantService?.GetConnectionString();
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                var provider = _tenantService.GetDatabaseProvider();
                if (provider.ToLower().Equals("pgsql"))
                {
                    optionsBuilder.UseNpgsql(_tenantService.GetConnectionString());
                }
            }
        }
    }
}