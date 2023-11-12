using System;
using System.Collections.Generic;
using AutoMapper;
using PlayersService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlayersService;
using ClubService;

namespace PlayersService.SyncDataServices.Grpc
{
    public class ClubDataClient : IClubDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ClubDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Club> ReturnAllClubs()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcClub"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcClub"]);
            var client = new GrpcClub.GrpcClubClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllClubs(request);
                return _mapper.Map<IEnumerable<Club>>(reply.Club);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}