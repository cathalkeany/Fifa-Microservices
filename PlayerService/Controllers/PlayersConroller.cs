using System;
using System.Collections.Generic;
using AutoMapper;
using PlayersService.Data;
using PlayersService.Dtos;
using PlayersService.Models;
using Microsoft.AspNetCore.Mvc;

namespace PlayersService.Controllers
{
    [Route("api/c/clubs/{clubId}/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepo _repository;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlayerReadDto>> GetPlayersForClub(int clubId)
        {
            Console.WriteLine($"--> Hit GetPlayersForClub: {clubId}");

            if (!_repository.ClubExists(clubId))
            {
                return NotFound();
            }

            var players = _repository.GetPlayersForClub(clubId);

            return Ok(_mapper.Map<IEnumerable<PlayerReadDto>>(players));
        }

        [HttpGet("{playerId}", Name = "GetPlayerForClub")]
        public ActionResult<PlayerReadDto> GetPlayerForClub(int clubId, int playerId)
        {
            Console.WriteLine($"--> Hit GetPlayerForClub: {clubId} / {playerId}");

            if (!_repository.ClubExists(clubId))
            {
                return NotFound();
            }

            var player = _repository.GetPlayer(clubId, playerId);

            if(player == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlayerReadDto>(player));
        }

        [HttpPost]
        public ActionResult<PlayerReadDto> CreatePlayerForClub(int clubId, PlayerCreateDto playerDto)
        {
             Console.WriteLine($"--> Hit CreatePlayerForClub: {clubId}");

            if (!_repository.ClubExists(clubId))
            {
                return NotFound();
            }

            var player = _mapper.Map<Player>(playerDto);

            _repository.CreatePlayer(clubId, player);
            _repository.SaveChanges();

            var playerReadDto = _mapper.Map<PlayerReadDto>(player);

            return CreatedAtRoute(nameof(GetPlayerForClub),
                new {clubId = clubId, playerId = playerReadDto.Id}, playerReadDto);
        }

    }
}