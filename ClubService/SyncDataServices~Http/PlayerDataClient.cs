using System.Text;
using System.Text.Json;
using ClubService.Dtos;

namespace ClubService.SynDataServices.Http
{
    public class PlayerDataClient : IPlayerDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PlayerDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendClubToPlayer(ClubReadDto club)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(club),
                Encoding.UTF8,
                "application/json"); 

            var response = await _httpClient.PostAsync($"{_config["PlayerService"]}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Post to player service was OK");
            }
            else
            {
                Console.WriteLine("--> Post to player service was NOT OK");
            }
        }
    }
}