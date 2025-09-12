using AutoMapper;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Entities.Infos;

namespace PortalGalaxy.Services.Profiles;

public class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        CreateMap<InstructorInfo, InstructorDtoResponse>();
    }
}
