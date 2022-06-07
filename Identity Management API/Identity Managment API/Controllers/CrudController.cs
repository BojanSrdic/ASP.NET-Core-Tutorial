using Identity_Managment_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Managment_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CrudController : ControllerBase
	{
		private readonly ICrudService _crudService;
		public CrudController(ICrudService crudService)
		{
			_crudService = crudService;
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			return Ok(_crudService.GetUsers());
		}

		[HttpGet("{id}")]
		public IActionResult GetUser(string id)
		{
			var user = _crudService.GetUser(id);

			if (user == null)
			{
				return NotFound("User doesn't exixt");
			}

			return Ok(user);
		}
	}
}
