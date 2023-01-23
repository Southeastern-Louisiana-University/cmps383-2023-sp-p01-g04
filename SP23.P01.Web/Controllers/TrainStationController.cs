using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Features;

namespace SP23.P01.Web.Controllers
{
    [ApiController]
    [Route("/api/stations")]
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
        [Route("/api/stations/{id}")]
        public ActionResult<TrainStationDto> GetTrainStationById(int id) {
            var result = GetTrainStationDtos(trainStations.Where(x => x.Id == id)).FirstOrDefault();
            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("/api/stations")]
        public ActionResult<TrainStationDto> createTrainStation(TrainStationDto dto) {
            if (IsInvalid(dto) ){
                return BadRequest();
            }

            if(dto.Name.Length > 120) {
                return BadRequest();
            }

            var newTrainStation = new TrainStation();
            newTrainStation.Name = dto.Name;
            newTrainStation.Address = dto.Address;

            trainStations.Add(newTrainStation);

            dataContext.SaveChanges();

            dto.Id= newTrainStation.Id;

            return CreatedAtAction(nameof(GetTrainStationById), new { id = dto.Id }, dto); ;
        }

        [HttpPut]
        [Route("/api/stations/{id}")]
        public ActionResult<TrainStationDto> updateTrainStation(int id, TrainStationDto dto) {
            if(IsInvalid(dto)) {
                return BadRequest();
            }

            if (dto.Name.Length > 120) {
                return BadRequest();
            }

            var trainStation = trainStations.FirstOrDefault(x=> x.Id == id);
            if(trainStation == null) {
                return BadRequest();
            }

            trainStation.Name = dto.Name;
            trainStation.Address = dto.Address;

            dataContext.SaveChanges();

            dto.Id = trainStation.Id;


            return Ok(dto);
        }


        public static IQueryable<TrainStationDto> GetTrainStationDtos(IQueryable<TrainStation> trainStations) {
            return trainStations
                .Select(x => new TrainStationDto {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                });
        }

        private static bool IsInvalid(TrainStationDto dto) {
            return string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Address);
        }
    }
}