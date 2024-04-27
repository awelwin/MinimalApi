using Microsoft.EntityFrameworkCore;
using EF = Alex.MinimalApi.Service.Repository.EntityFramework;

namespace Alex.MinimalApi.Service.Repository.EntityFramework
{
    public class MinimalApiDbContext : DbContext
    {
        public MinimalApiDbContext(DbContextOptions<MinimalApiDbContext> options) : base(options) { }

        public virtual DbSet<EF.Employee> Employees { get; set; }
        public virtual DbSet<EF.Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// Intercept + Inject entity metadata where needed to automate things like event timestamps "created"
        /// </summary>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //created field
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                E.Property("Created").CurrentValue = DateTime.UtcNow;
            });

            //updated field
            var updatedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            updatedEntities.ForEach(E =>
            {
                E.Property("Updated").CurrentValue = DateTime.UtcNow;
                E.Property("Created").IsModified = false;

            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Deleted).ToList();
            EditedEntities.ForEach(E =>
            {
                E.Property("Updated").CurrentValue = DateTime.UtcNow;
                E.Property("Created").IsModified = false;

            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
