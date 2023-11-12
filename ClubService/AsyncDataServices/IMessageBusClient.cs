using ClubService.Dtos;

namespace ClubService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewClub(ClubPublishedDto clubPublishedDto);
    }
}