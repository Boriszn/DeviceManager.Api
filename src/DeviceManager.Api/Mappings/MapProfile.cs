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
    public class MappingProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class
        /// </summary>
        public MappingProfile()
        {
            // Device ViewModel To Device
            this.CreateMap<DeviceViewModel, Device>()
                .ForMember(dest => dest.DeviceTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => Guid.NewGuid()))
                ;
        }
    }
}
