using DeviceManager.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Api.Data
{
    /// <summary>
    /// The device DB (entity framework's) context
    /// </summary>
    public class DeviceContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DeviceContext(DbContextOptions<DeviceContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Get or sets the devices data model
        /// </summary>
        public DbSet<Device> Devices { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(contact => new { contact.DeviceId });
        }
    }
}
