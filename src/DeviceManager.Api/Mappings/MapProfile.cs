using System;
using AutoMapper.Configuration;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;

namespace DeviceManager.Api.Mappings
{
    /// <summary>
    /// Contains objects mapping
    /// </summary>
    /// <seealso cref="AutoMapper.Configuration.MapperConfigurationExpression" />
    public class MapsProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapsProfile"/> class
        /// </summary>
        public MapsProfile()
        {
            // Device ViewModel To Device
            this.CreateMap<DeviceViewModel, Device>()
                .ForMember(dest => dest.DeviceTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DeviceCode, opt => opt.MapFrom(src => src.DeviceCode))
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DeviceGroup, opt => opt.MapFrom(src => src));

            // Device ViewModel to DeviceDetail
            this.CreateMap<DeviceViewModel, DeviceGroup>()
                .ForMember(dest => dest.DeviceGroupId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.OperatingSystem, opt => opt.MapFrom(src => src.OperatingSystem));

            // Maps Device to Device ViewModel
            this.CreateMap<Device, DeviceViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.DeviceTitle))
                .ForMember(dest => dest.DeviceCode, opt => opt.MapFrom(src => src.DeviceCode))
                .AfterMap((src, dest, context) => context.Mapper.Map(src.DeviceGroup, dest));


            // Maps Device Group to Device ViewModel
            this.CreateMap<DeviceGroup, DeviceViewModel>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.OperatingSystem, opt => opt.MapFrom(src => src.OperatingSystem));

        }
    }
}
