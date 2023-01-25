using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Controllers
{

    [ApiController]
    [Route("/api/trainstations")]
    public class TrainStationController: ControllerBase
	{

		private readonly DataContext dataContext;
        private readonly DbSet<TrainStation> trainStations;

        public TrainStationController(DataContext dataContext)
		{
			this.dataContext = dataContext;
			trainStations = dataContext.Set<TrainStation>();
		}

		[HttpGet]
		public IQueryable<TrainStationDto> GetAll()
		{
			return GetTrainStationDtos(trainStations);
		}

        private static IQueryable<TrainStationDto> GetTrainStationDtos(IQueryable<TrainStation> trainStations)
        {
            return trainStations
                .Select(x => new TrainStationDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                });
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TrainStationDto> GetTrainStationById(int id)
        {
            var result = GetTrainStationDtos(trainStations.Where(x => x.Id == id)).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost]
        public ActionResult<TrainStationDto> CreateTrainStation(TrainStationDto dto)
        {
            if (IsInvalid(dto))
            {
                return BadRequest();
            }

            var trainStation = new TrainStation
            {
                Name = dto.Name!,
                Address = dto.Address
            };

            trainStations.Add(trainStation);

            dataContext.SaveChanges();

            dto.Id = trainStation.Id;

            return CreatedAtAction
                (nameof(GetTrainStationById),
                new { id = dto.Id },
                dto);
        }

        private static bool IsInvalid(TrainStationDto dto)
        {
            return string.IsNullOrWhiteSpace(dto.Name) ||
                   dto.Name.Length > 120 ||
                   string.IsNullOrWhiteSpace(dto.Address);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<TrainStationDto> UpdateTrainStation(int id, TrainStationDto dto)
        {
            var trainStation = trainStations.FirstOrDefault(x => x.Id == id);
            if (trainStation == null)
            {
                return NotFound();
            }

            if (IsInvalid(dto))
            {
                return BadRequest();
            }

            trainStation.Name = dto.Name!;
            trainStation.Address = dto.Address;

            dataContext.SaveChanges();

            dto.Id = trainStation.Id;

            return Ok(dto);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var trainStation = trainStations.FirstOrDefault(x => x.Id == id);
            if (trainStation == null)
            {
                return NotFound();
            }

            trainStations.Remove(trainStation);

            dataContext.SaveChanges();

            return Ok();
        }


    }
}

