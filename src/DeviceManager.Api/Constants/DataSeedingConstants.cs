using System;

namespace DeviceManager.Api.Constants
{
    /// <summary>
    /// Intended for EF data seeding 
    /// </summary>
    public static class DataSeedingDefaultConstants
    {
        /// <summary>
        /// Company name
        /// </summary>
        public const string SeedDeviceGroupCompany = "Microsoft";

        /// <summary>
        /// Operating System
        /// </summary>
        public const string SeedDeviceGroupOs = "Windows 10";

        /// <summary>
        /// Device code
        /// </summary>
        public const string SeedDevice1Title = "X Box";

        /// <summary>
        /// Device code
        /// </summary>
        public const string SeedDevice1Code = "Xbox1234";

        /// <summary>
        /// Device title
        /// </summary>
        public const string SeedDevice2Title = "Surface Tablet";

        /// <summary>
        /// Device code
        /// </summary>
        public const string SeedDevice2Code = "Surface568";

        /// <summary>
        /// Guid of the device group
        /// </summary>
        public static readonly Guid SeedDeviceGroupId = new Guid("843a92af-9174-49a3-a2e7-08f99919d6ca");

        /// <summary>
        /// Device code
        /// </summary>
        public static readonly Guid SeedDevice1Id = new Guid("9b34ae90-f226-43df-8ad0-7cfdce2f16a7");

        /// <summary>
        /// Second seed device Guid
        /// </summary>
        public static readonly Guid SeedDevice2Id = new Guid("1ee62dd5-d698-4e67-a260-f5a66f86f0df");
    }
}