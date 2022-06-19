using Excel_Management_API.Data;
using Excel_Management_API.ExcelManagementService;
using Excel_Management_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Excel_Management_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExcelHandlerController : ControllerBase
	{
		[HttpPost]
		public async Task ExportExcelFile()
		{
			// Set licence 
			ExcelHandlerService.ExcelLicence();

			// Set file location
			var file = new FileInfo(@"C:\Users\b.srdic\Desktop\ASP.NET Core\Excel Management API\file.xlsx");

			// Set data for export
			var people = InMemoryDbProvider.SeedData();

			// Create new excel file
			await ExcelHandlerService.CreateExcelFile(people, file);

			// Download excel file
		}

		[HttpGet]
		public async Task ReadDataFromExcelFile()
		{
			// Creaete file locatiom
			var file = new FileInfo(@"C:\Users\b.srdic\Desktop\ASP.NET Core\Excel Management API\file.xlsx");

			// Read data from excel
			List<User> peopleFormExcel = await ExcelHandlerService.LoadExcelFile(file);

			foreach (var person in peopleFormExcel)
			{
				Console.WriteLine($"{person.Id} {person.Username} {person.Email}");
			}
		}
	}
}
