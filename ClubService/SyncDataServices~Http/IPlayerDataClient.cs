using ClubService.Dtos;

namespace ClubService.SynDataServices.Http
{
    public interface IPlayerDataClient
    {
        Task SendClubToPlayer(ClubReadDto club);
    }
}