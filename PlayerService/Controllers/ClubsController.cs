using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayersService.Data;
using PlayersService.Dtos;

namespace PlayersService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IPlayerRepo _repository;
        private readonly IMapper _mapper;

        public ClubsController(IPlayerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClubReadDto>> GetClubs()
        {
            Console.WriteLine("--> Getting Clubs from PlayerService");

            var clubItems = _repository.GetAllClubs();

            return Ok(_mapper.Map<IEnumerable<ClubReadDto>>(clubItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Player Service");

            return Ok("Inbound test of from Clubs Controler");
        }
    }
}