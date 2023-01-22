using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Features;

namespace SP23.P01.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainStationController : ControllerBase{
        

        private readonly ILogger<TrainStationController> _logger;
        private readonly DbSet<TrainStation> trainStations;
        private readonly DataContext dataContext;

        public TrainStationController(ILogger<TrainStationController> logger, DataContext datacontext){
            _logger = logger;
            this.dataContext = datacontext;
            trainStations = datacontext.Set<TrainStation>();
        }

        [HttpGet]
        public IQueryable<TrainStationDto> GetAll(){
            return GetTrainStationDtos(trainStations);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TrainStationDto> GetItemById(int id) {
            var result = GetTrainStationDtos(trainStations.Where(x => x.Id == id)).FirstOrDefault();
            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }


        public static IQueryable<TrainStationDto> GetTrainStationDtos(IQueryable<TrainStation> trainStations) {
            return trainStations
                .Select(x => new TrainStationDto {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                });
        }
    }
}