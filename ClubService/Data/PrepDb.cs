using ClubService.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {   
            if(isProd)
            {
                Console.WriteLine("Running DB Migrations..");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if(!context.Clubs.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Clubs.AddRange(
                    new Club() {Name="Liverpool", Country="England", League="Premier League"},
                    new Club() {Name="Real Madrid", Country="Spain",  League="La Liga"},
                    new Club() {Name="Bayern Munich", Country="Germany",  League="Bundesliga"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}