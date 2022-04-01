using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Contracts;
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService): base(options)
        {
            _tenantService = tenantService;
            TenantId = _tenantService.GetTenant()?.TID;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasQueryFilter(a => a.TenantId == TenantId);
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = TenantId;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}