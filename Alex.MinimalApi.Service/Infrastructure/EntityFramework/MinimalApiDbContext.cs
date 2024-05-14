using Microsoft.EntityFrameworkCore;

namespace Alex.MinimalApi.Service.Infrastructure.EntityFramework
{
    public class MinimalApiDbContext : DbContext
    {
        public MinimalApiDbContext(DbContextOptions<MinimalApiDbContext> options) : base(options) { }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>(x =>
            {
                x.HasKey(y => y.Id);
                x.OwnsOne(y => y.TaxFile, tf =>
                {
                    tf.WithOwner().HasForeignKey("EmployeeId");
                    tf.Property<int>("Id");
                    tf.HasKey("Id");
                    tf.ToTable("TaxFile");
                    tf.OwnsMany(x => x.TaxFileRecords, tfr =>
                    {
                        tfr.WithOwner().HasForeignKey("TaxFileId");
                        tfr.Property<int>("Id");
                        tfr.ToTable("TaxFileRecord");
                        tfr.HasKey("Id");
                    });
                });
            });

        }


        /// <summary>
        /// Intercept + Inject entity metadata where needed to automate things like event timestamps "created"
        /// </summary>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
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
