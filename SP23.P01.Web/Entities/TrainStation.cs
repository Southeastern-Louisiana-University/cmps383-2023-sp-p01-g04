using System;
namespace SP23.P01.Web.Entities
{
	public class TrainStation
	{

		public int Id { get; set; }

		//[Required, MaxLenght(120)]
		public string? Name { get; set; }

		public string? Address { get; set; }


	}
}

