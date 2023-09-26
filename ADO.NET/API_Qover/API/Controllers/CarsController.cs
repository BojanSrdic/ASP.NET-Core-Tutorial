using API.Models;
using API.Models.DTOs;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase
	{

		private readonly ICarService _carService;

		public CarsController(ICarService carService)
		{
			_carService = carService;
		}

		// GET: api/Cars
		[HttpGet]
		public IActionResult GetList()
		{
			return Ok(_carService.GetCarListFromDB());
		}

		// POST: api/Cars
		[HttpPost]
		public IActionResult Create(CarModelDto model)
		{
			_carService.CreateCarObject(model);

			return Ok(new { message = "User created successfully!" });
		}

		// DELETE: api/user/id
		[HttpGet("id")]
		public IActionResult GetById(int id)
		{
			var item = _carService.GetCar(id);

			if (item == null)
				return NotFound("User doesn't exist");

			return Ok(item);
		}

	}
}
