using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Api.Data.DataSeed
{
    /// <inheritdoc/>
    public class DataSeeder : IDataSeeder
    {
        /// <inheritdoc/>
        public void SeedData(ModelBuilder modelBuilder)
        {
            // Add a new device group and add devices to the same
            modelBuilder.Entity<DeviceGroup>().HasData(new DeviceGroup
            {
                DeviceGroupId = Constants.SeedDeviceGroupId,
                Company = Constants.SeedDeviceGroupCompany,
                OperatingSystem = Constants.SeedDeviceGroupOS
            });

            // Devices can be added using Device group also
            modelBuilder.Entity<Device>().HasData(new Device
            {
                DeviceId = Constants.SeedDevice2Id,
                DeviceCode = Constants.SeedDevice2Code,
                DeviceTitle = Constants.SeedDevice2Title,
                DeviceGroupId = Constants.SeedDeviceGroupId
            }, new Device
            {

                DeviceId = Constants.SeedDevice1Id,
                DeviceCode = Constants.SeedDevice1Code,
                DeviceTitle = Constants.SeedDevice1Title,
                DeviceGroupId = Constants.SeedDeviceGroupId
            });
        }
    }
}
