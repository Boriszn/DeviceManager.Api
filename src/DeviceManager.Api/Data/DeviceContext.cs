using DeviceManager.Api.Data.DataSeed;
using DeviceManager.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Api.Data
{
    /// <summary>
    /// The device DB (entity framework's) context
    /// </summary>
    public class DeviceContext : DbContext, IDbContext
    {
        private readonly IDataSeeder dataSeeder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="dataSeeder">Initial data seeder</param>
        public DeviceContext(DbContextOptions<DeviceContext> options, IDataSeeder dataSeeder)
            : base(options)
        {
            this.dataSeeder = dataSeeder;
            // TODO: Comment below this if you are running migrations commands
            // TODO: uncomment below line of you are running the application for the first time
            //this.Database.EnsureCreated();
        }

        /// <summary>
        /// Get or sets the devices data model
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Get or sets the device groups data model
        /// </summary>
        public DbSet<DeviceGroup> DeviceGroups { get; set; }

        /// <summary>
        /// Relation between tables.
        /// </summary>
        /// <param name="modelBuilder">Entity framework model builder before creating database</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(device => new { device.DeviceId });

            // No need to define the relation explicitly as long as conventions are followed.

            //modelBuilder.Entity<Device>()
            //    .HasOne(device => device.DeviceGroup)
            //    .WithMany(group => group.Devices)
            //    .HasForeignKey(device => device.DeviceGroupId);

            // Call Data seeder
            this.dataSeeder.SeedData(modelBuilder);
        }
    }
}
