using AutoMapper;
using CMS.Core.Model;
using CMS.Service.Dtos.RestrictedAccessTime;
using CMS.Service.Dtos.RestrictedIP;
using CMS.Service.Dtos.UserLog;
using DateConverter;

namespace CMS.Service.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region UserLog
            CreateMap<UserLog, UserLogDto>()
                .ForMember(dest => dest.UserName
                    , opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Date
                    , opt => opt.MapFrom(src => src.Date.GregorianToPersian(Converter.IncludeTime.No)));

            #endregion

            #region RestrictedIP
            CreateMap<RestrictedIP, RestrictedIPDto>();
            #endregion

            #region RestrictedAccessTime
            CreateMap<RestrictedAccessTime, RestrictedAccessTimeDto>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date.HasValue
                        ? src.Date.Value.GregorianToPersian(Converter.IncludeTime.No) : "No Restriction"))
                .ForMember(dest => dest.FromTime,
                    opt => opt.MapFrom(src => src.FromTime.ToString("hh\\:mm\\:ss")))
                .ForMember(dest => dest.ToTime,
                    opt => opt.MapFrom(src => src.ToTime.ToString("hh\\:mm\\:ss")));
            #endregion
        }
    }
}
