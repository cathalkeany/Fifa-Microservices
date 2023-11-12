using PlayersService.Models;

namespace PlayersService.Data
{
    public class PlayerRepo : IPlayerRepo
    {
        private readonly AppDbContext _context;

        public PlayerRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool ClubExists(int clubId)
        {
            return _context.Clubs.Any(c => c.Id == clubId);
        }

        public void CreateClub(Club club)
        {
            if(club == null)
            {
                throw new ArgumentNullException(nameof(club));
            }
            _context.Clubs.Add(club);
        }

        public void CreatePlayer(int clubId, Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            player.ClubId = clubId;
            _context.Players.Add(player);
        }

        public bool ExternalClubExists(int externalClubId)
        {
            return _context.Clubs.Any(c => c.ExternalID == externalClubId);
        }

        public IEnumerable<Club> GetAllClubs()
        {
            return _context.Clubs.ToList();
        }

        public Player GetPlayer(int clubId, int playerId)
        {
            return _context.Players
                .Where(c => c.ClubId == clubId && c.Id == playerId).FirstOrDefault();
        }

        public IEnumerable<Player> GetPlayersForClub(int clubId)
        {
            return _context.Players
                .Where(c => c.ClubId == clubId)
                .OrderBy(c => c.Club.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}