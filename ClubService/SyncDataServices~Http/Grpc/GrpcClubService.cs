using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using ClubService.Data;

namespace ClubService.SyncDataServices.Grpc
{
    public class GrpcClubService : GrpcClub.GrpcClubBase
    {
        private readonly IClubRepo _repository;
        private readonly IMapper _mapper;

        public GrpcClubService(IClubRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<ClubResponse> GetAllClubs(GetAllRequest request, ServerCallContext context)
        {
            var response = new ClubResponse();
            var clubs = _repository.GetAllClubs();

            foreach(var club in clubs)
            {
                response.Club.Add(_mapper.Map<GrpcClubModel>(club));
            }

            return Task.FromResult(response);
        }
    }
}