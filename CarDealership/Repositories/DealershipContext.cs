using Microsoft.EntityFrameworkCore;
using CarDealership.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CarDealership.Repositories
{
    public class DealershipContext : DbContext
    {
        public DealershipContext(DbContextOptions<DealershipContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<BodyStyle> BodyStyles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            runTriggers();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            runTriggers();

            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// This will run anything that should be triggered by CarDealership.SaveChanges();
        /// </summary>
        private void runTriggers()
        {
            updateModelBaseFields();
        }

        /// <summary>
        /// This will update DateCreated/DateModified & UserCreated/UserModified fields accordingly
        /// </summary>
        private void updateModelBaseFields()
        {
            var changedModelBaseEntries = ChangeTracker.Entries().Where(x => x.Entity is ModelBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in changedModelBaseEntries)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModelBase)entity.Entity).DateCreated = DateTimeOffset.UtcNow;
                }

                ((ModelBase)entity.Entity).DateModified = DateTimeOffset.UtcNow;
            }
        }
    }
}