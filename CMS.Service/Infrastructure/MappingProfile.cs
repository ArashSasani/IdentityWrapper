using AutoMapper;
using CMS.Core.Model;
using CMS.Service.Dtos.RestrictedAccessTime;
using CMS.Service.Dtos.RestrictedIP;
using CMS.Service.Dtos.UserLog;

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
                    , opt => opt.MapFrom(src => src.Date.ToShortDateString()));

            #endregion

            #region RestrictedIP
            CreateMap<RestrictedIP, RestrictedIPDto>();
            #endregion

            #region RestrictedAccessTime
            CreateMap<RestrictedAccessTime, RestrictedAccessTimeDto>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date.HasValue
                        ? src.Date.Value.ToShortDateString() : "No Restriction"))
                .ForMember(dest => dest.FromTime,
                    opt => opt.MapFrom(src => src.FromTime.ToString("hh\\:mm\\:ss")))
                .ForMember(dest => dest.ToTime,
                    opt => opt.MapFrom(src => src.ToTime.ToString("hh\\:mm\\:ss")));
            #endregion
        }
    }
}
