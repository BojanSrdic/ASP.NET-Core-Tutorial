using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exception_Handling.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExceptionHandlingController : ControllerBase
	{
		// POST: api/user
		[HttpPost]
		public IActionResult UsingTryChachBlock(int number)
		{
			try {
				var result = 100 / number;
			} catch (Exception ex) {
				Console.WriteLine(ex);	// exception is writen in logger
				return StatusCode(500, "You can't devide by zero!");
			}
			
			return Ok(" Enterd number is not zero, exception doesen't need to be handeld!");
		}

		// POST: api/user
		[HttpPost("ubim")]
		public IActionResult UsingBuiltInMiddleware(int number)
		{
			var result = 100 / number;
			return Ok(" Enterd number is not zero, exception doesen't need to be handeld!");
		}

		// POST: api/user
		[HttpPost("ucehm")]
		public IActionResult UsingCustomExceptionHandlingMiddleware(int number)
		{
			var result = 100 / number;
			return Ok(" Enterd number is not zero, exception doesen't need to be handeld!");
		}

	}
}

// https://www.youtube.com/watch?v=tk1QK71DVtg
// https://www.youtube.com/watch?v=95EbHz3aKYA
// https://www.youtube.com/watch?v=NjOC4q_57VI
// https://www.youtube.com/watch?v=jeBttUIqpuc
// https://www.youtube.com/watch?v=Cy53ENszjWo
// https://www.youtube.com/watch?v=9qHb-2Edg7o
// Deket https://www.c-sharpcorner.com/article/exception-handling-in-dotnetcore/
// exception hierarchy in c#
