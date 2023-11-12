using AutoMapper;
using ClubService.AsyncDataServices;
using ClubService.Data;
using ClubService.Dtos;
using ClubService.Models;
using ClubService.SynDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubServiceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IClubRepo _repository;
        private readonly IMapper _mapper;
        private readonly IPlayerDataClient _dataClient;
        private readonly IMessageBusClient _messageBusClient;

        public ClubsController(
            IClubRepo repository, 
            IMapper mapper,
            IPlayerDataClient dataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _dataClient = dataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClubReadDto>> GetClubs()
        {
            Console.WriteLine("--> Getting Clubs....");

            var clubItem = _repository.GetAllClubs();

            return Ok(_mapper.Map<IEnumerable<ClubReadDto>>(clubItem));
        }

        [HttpGet("{id}", Name = "GetClubById")]
        public ActionResult<ClubReadDto> GetClubById(int id)
        {
            var clubItem = _repository.GetClubById(id);
            if (clubItem != null)
            {
                return Ok(_mapper.Map<ClubReadDto>(clubItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ClubReadDto>> CreateClub(ClubCreateDto clubCreateDto)
        {
            var clubModel = _mapper.Map<Club>(clubCreateDto);
            _repository.CreateClub(clubModel);
            _repository.SaveChanges();

            var clubReadDto = _mapper.Map<ClubReadDto>(clubModel);

            //sync post
            try
            {
                await _dataClient.SendClubToPlayer(clubReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //async message
            try
            {
                var clubPublishedDto = _mapper.Map<ClubPublishedDto>(clubReadDto);
                clubPublishedDto.Event = "Club_Published";

                _messageBusClient.PublishNewClub(clubPublishedDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return CreatedAtRoute(nameof(GetClubById), new { Id = clubReadDto.Id}, clubReadDto);
        }
    }
}