using ClubService.Models;

namespace ClubService.Data
{
    public class ClubRepo : IClubRepo
    {
        private readonly AppDbContext _context;

        public ClubRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateClub(Club club)
        {
            if(club == null)
            {
                throw new ArgumentNullException(nameof(club));
            }
            
            _context.Add(club);
        }

        public IEnumerable<Club> GetAllClubs()
        {
            return _context.Clubs.ToList();
        }

        public Club GetClubById(int Id)
        {
            return _context.Clubs.FirstOrDefault(t => t.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}