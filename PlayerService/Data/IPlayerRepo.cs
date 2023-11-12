using PlayersService.Models;

namespace PlayersService.Data
{
    public interface IPlayerRepo
    {
        bool SaveChanges();

        //clubs stuff
        IEnumerable<Club> GetAllClubs();
        void CreateClub(Club club);
        bool ClubExists(int clubId);
        bool ExternalClubExists(int externalClubId);

        //players stuff
        IEnumerable<Player> GetPlayersForClub(int clubId);
        Player GetPlayer(int clubId, int playerId);
        void CreatePlayer(int clubId, Player player);
    }
}