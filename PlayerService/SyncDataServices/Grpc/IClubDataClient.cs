using System.Collections.Generic;
using PlayersService.Models;

namespace PlayersService.SyncDataServices.Grpc
{
    public interface IClubDataClient
    {
        IEnumerable<Club> ReturnAllClubs();
    }
}