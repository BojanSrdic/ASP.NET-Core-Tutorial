using CRUD_Solid.DataTransferObjects;
using CRUD_Solid.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Solid.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		// POST: api/user
		[HttpPost]
		public IActionResult Create(UserDtoModel model)
		{
			_userService.CreateUser(model);

			return Ok(new { message = "User created successfully!" });
		}

		// GET: api/user/id
		[HttpDelete("id")]
		public IActionResult Delete(int id)
		{
			var item = _userService.GetUser(id);

			if (item == null)
				return NotFound("User doesn't exist");


			_userService.DeleteUser(item.Id);

			return Ok(new { message = "User deleted successfully!" });
		}

		// GET: api/user
		[HttpGet]
		public IActionResult GetList()
		{
			return Ok(_userService.GetUsers());
		}

		// DELETE: api/user/id
		[HttpGet("id")]
		public IActionResult GetById(int id)
		{
			var item = _userService.GetUser(id);

			if(item == null)
				return NotFound("User doesn't exist");

			var read = new UserDtoModel { UserName = item.UserName, Email = item.Email };

			return Ok(read);
		}

	}
}
