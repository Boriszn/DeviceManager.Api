using DeviceManager.Api.Constants;
using DeviceManager.Api.Data.Model;
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
                DeviceGroupId = DataSeedingDefaultConstants.SeedDeviceGroupId,
                Company = DataSeedingDefaultConstants.SeedDeviceGroupCompany,
                OperatingSystem = DataSeedingDefaultConstants.SeedDeviceGroupOs
            });

            // Devices can be added using Device group also
            modelBuilder.Entity<Device>().HasData(new Device
            {
                DeviceId = DataSeedingDefaultConstants.SeedDevice2Id,
                DeviceCode = DataSeedingDefaultConstants.SeedDevice2Code,
                DeviceTitle = DataSeedingDefaultConstants.SeedDevice2Title,
                DeviceGroupId = DataSeedingDefaultConstants.SeedDeviceGroupId
            }, new Device
            {

                DeviceId = DataSeedingDefaultConstants.SeedDevice1Id,
                DeviceCode = DataSeedingDefaultConstants.SeedDevice1Code,
                DeviceTitle = DataSeedingDefaultConstants.SeedDevice1Title,
                DeviceGroupId = DataSeedingDefaultConstants.SeedDeviceGroupId
            });
        }
    }
}
