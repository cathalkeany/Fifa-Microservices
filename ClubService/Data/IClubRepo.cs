using ClubService.Models;

namespace ClubService.Data
{
    public interface IClubRepo
    {
        bool SaveChanges();
        IEnumerable<Club> GetAllClubs();
        Club GetClubById(int Id);
        void CreateClub(Club club);
    }
}