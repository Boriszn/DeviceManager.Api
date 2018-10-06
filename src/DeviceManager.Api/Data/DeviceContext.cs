using DeviceManager.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Api.Data
{
    /// <summary>
    /// The device DB (entity framework's) context
    /// </summary>
    public class DeviceContext : DbContext, IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DeviceContext(DbContextOptions<DeviceContext> options)
            : base(options)
        {
            // TODO: Comment below this if you are running migrations commands
            // TODO: uncomment below line of you are running the application for the first time
            //this.Database.EnsureCreated();
        }

        /// <summary>
        /// Get or sets the devices data model
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Relation between tables.
        /// </summary>
        /// <param name="modelBuilder">Entity framework model builder before creating database</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(contact => new { contact.DeviceId });

            // No need to define the relation explicitly as long as conventions are followed.

            //modelBuilder.Entity<Device>()
            //    .HasOne(device => device.DeviceGroup)
            //    .WithMany(group => group.Devices)
            //    .HasForeignKey(device => device.DeviceGroupId);
        }
    }
}
