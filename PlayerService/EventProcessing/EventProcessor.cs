using System;
using System.Text.Json;
using AutoMapper;
using PlayersService.Data;
using PlayersService.Dtos;
using PlayersService.Models;
using Microsoft.Extensions.DependencyInjection;
using PlayersServiceService.Dtos;

namespace PlayersService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ClubPublished:
                    addClub(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Club_Published":
                    Console.WriteLine("--> Club Published Event Detected");
                    return EventType.ClubPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addClub(string clubPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IPlayerRepo>();
                
                var clubPublishedDto = JsonSerializer.Deserialize<ClubPublishedDto>(clubPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Club>(clubPublishedDto);
                    if(!repo.ExternalClubExists(plat.ExternalID))
                    {
                        repo.CreateClub(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Club added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Club already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Club to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        ClubPublished,
        Undetermined
    }
}