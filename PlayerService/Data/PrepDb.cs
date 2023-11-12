using System;
using System.Collections.Generic;
using PlayersService.Models;
using PlayersService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PlayersService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IClubDataClient>();

                var clubs = grpcClient.ReturnAllClubs();

                SeedData(serviceScope.ServiceProvider.GetService<IPlayerRepo>(), clubs);
            }
        }
        
        private static void SeedData(IPlayerRepo repo, IEnumerable<Club> clubs)
        {
            Console.WriteLine("Seeding new clubs...");

            foreach (var club in clubs)
            {
                if(!repo.ExternalClubExists(club.ExternalID))
                {
                    repo.CreateClub(club);
                }
                repo.SaveChanges();
            }
        }
    }
}