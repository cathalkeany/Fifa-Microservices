using AutoMapper;
using PlayersService.Dtos;
using PlayersService.Models;
using PlayersService;
using ClubService;

namespace PlayersService.Profiles
{
    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            // Source -> Target
            CreateMap<Club, ClubReadDto>();
            CreateMap<PlayerCreateDto, Player>();
            CreateMap<Player, PlayerReadDto>();
            CreateMap<ClubPublishedDto, Club>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcClubModel, Club>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.ClubId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Players, opt => opt.Ignore());
        }
    }
}