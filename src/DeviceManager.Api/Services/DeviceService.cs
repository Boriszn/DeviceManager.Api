using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Management.Dapper;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDeviceValidationService deviceValidationService;
        private readonly IDapperUnitOfWork dapperUnitOfWork;

        /// <inheritdoc />
        public DeviceService(
            IUnitOfWork unitOfWork,
            IDapperUnitOfWork dapperUnitOfWork,
            IDeviceValidationService deviceValidationService,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dapperUnitOfWork = dapperUnitOfWork;
            this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
        }

        #region Entity Framework Core

        /// <inheritdoc />
        public List<DeviceViewModel> GetDevices(int page, int pageSize)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            var devices = deviceRepository.GetAll(page, pageSize, x => x.DeviceGroup).ToList();

            return mapper.Map<List<DeviceViewModel>>(devices);
        }

        /// <inheritdoc />
        public DeviceViewModel GetDeviceById(Guid deviceId)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            var deviceData = deviceRepository.Get(deviceId, device => device.DeviceGroup);

            return mapper.Map<DeviceViewModel>(deviceData);
        }

        /// <inheritdoc />
        public async Task<DeviceViewModel> GetDeviceByIdAsync(Guid deviceId)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            var deviceData = await deviceRepository.GetAsync(deviceId, device => device.DeviceGroup);

            return mapper.Map<DeviceViewModel>(deviceData);
        }

        /// <inheritdoc />
        public DeviceViewModel GetDeviceByTitle(string deviceTitle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void CreateDevice(DeviceViewModel deviceViewModel)
        {
            // Validate input
            deviceValidationService
                .Validate(deviceViewModel);

            var deviceRepository = unitOfWork.GetRepository<Device>();

            deviceRepository.Add(mapper.Map<DeviceViewModel, Device>(deviceViewModel));

            // Commit changes
            unitOfWork.Commit();
        }

        /// <inheritdoc />
        public void UpdateDevice(Guid deviceId, DeviceViewModel deviceViewModel)
        {
            // Validate input
            deviceValidationService
                .Validate(deviceViewModel)
                .ValidateDeviceId(deviceId);

            // Construct repository
            var deviceRepository = unitOfWork.GetRepository<Device>();

            // Get device
            Device device = deviceRepository.Get(deviceId);

            if (device == null)
            {
                throw new NullReferenceException();
            }

            device.DeviceTitle = deviceViewModel.Title;

            deviceRepository.Update(device);

            // Commit changes
            unitOfWork.Commit();
        }

        #endregion

        #region Dapper

        /// <inheritdoc />
        public async Task<IList<DeviceViewModel>> GetDevicesUsingDapper(int page, int pageSize)
        {
            dapperUnitOfWork.BeginTransaction();
            var deviceRepository = dapperUnitOfWork.GetRepository<Device>();
            var devices = await deviceRepository.AllAsync(page, pageSize);
            return mapper.Map<List<DeviceViewModel>>(devices);
        }


        /// <inheritdoc />
        public async Task<DeviceViewModel> GetDeviceByIdUsingDapperAsync(Guid deviceId)
        {
            dapperUnitOfWork.BeginTransaction();
            var deviceRepository = dapperUnitOfWork.GetRepository<Device>();

            var deviceData = await deviceRepository.FindAsync(deviceId);

            return mapper.Map<DeviceViewModel>(deviceData);
        }

        /// <inheritdoc />
        public async Task CreateDeviceUsingDapperAsync(DeviceViewModel deviceViewModel)
        {
            // Validate input
            deviceValidationService
                .Validate(deviceViewModel);


            var device = mapper.Map<DeviceViewModel, Device>(deviceViewModel);

            dapperUnitOfWork.BeginTransaction();

            if (device.DeviceGroupId.Equals(Guid.Empty))
            {
                var deviceGroupRepository = dapperUnitOfWork.GetRepository<DeviceGroup>();
                device.DeviceGroupId = device.DeviceGroup.DeviceGroupId;
                await deviceGroupRepository.AddAsync(device.DeviceGroup);
            }
            
            var deviceRepository = dapperUnitOfWork.GetRepository<Device>();
            device.DeviceId = Guid.NewGuid();
            await deviceRepository.AddAsync(device);

            // Commit changes
            dapperUnitOfWork.Commit();
        }


        #endregion

    }
}
