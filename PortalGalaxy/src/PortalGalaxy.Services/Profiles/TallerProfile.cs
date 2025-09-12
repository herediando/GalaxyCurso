using AutoMapper;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Entities.Infos;

namespace PortalGalaxy.Services.Profiles;

public class TallerProfile : Profile
{
    public TallerProfile()
    {
        CreateMap<TallerInfo, TallerDtoResponse>()
        .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("dd/MM/yyyy")));
    }
}
