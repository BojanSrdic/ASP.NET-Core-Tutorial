using API.Models;
using API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
	public interface ICarService
	{
		List<CarModel> GetCarListFromDB();
		CarModel GetCar(int id);
		void CreateCarObject(CarModelDto model);
	}
}
