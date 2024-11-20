namespace EagleRockService.Infra
{
    using AutoMapper;
    using EagleRockService.Features;
    using EagleRockService.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostDataRequest, EagleBot>();
        }
    }

}
