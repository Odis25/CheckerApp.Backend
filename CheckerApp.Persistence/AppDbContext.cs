using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Common;
using CheckerApp.Domain.Entities.CheckEntities;
using CheckerApp.Domain.Entities.ContractEntities;
using CheckerApp.Domain.Entities.HardwareEntities;
using CheckerApp.Domain.Entities.SoftwareEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> option,
            ICurrentUserService currentUserService)
            : base(option) =>
            _currentUserService = currentUserService;
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<CheckParameter> CheckParameters { get; set; }
        public DbSet<HardwareCheck> HardwareChecks { get; set; }
        public DbSet<SoftwareCheck> SoftwareChecks { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.LastModified = DateTime.Now;
                        entity.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Added:
                        entity.Entity.Created = DateTime.Now;
                        entity.Entity.CreatedBy = _currentUserService.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
