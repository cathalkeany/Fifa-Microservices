using AutoMapper;
using ClubService.Dtos;
using ClubService.Models;

namespace ClubService.Profiles
{
    public class ClubsProfile : Profile
    {
        public ClubsProfile()
        {
            //Source -> Target
            CreateMap<Club, ClubReadDto>();
            CreateMap<ClubCreateDto, Club>();
            CreateMap<ClubReadDto, ClubPublishedDto>();
            CreateMap<Club, GrpcClubModel>()
                .ForMember(dest => dest.ClubId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name));
        }
    }
}