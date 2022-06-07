using Identity_Managment_API.DataTransferObjects;
using Identity_Managment_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Managment_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _authService;

		public AuthenticationController(IAuthenticationService authService)
		{
			_authService = authService;
		}

		// POST: api/authentication/register
		[HttpPost("register")]
		[AllowAnonymous]
		public IActionResult Register(SignUp model)
		{
			var result = _authService.RegisterUser(model);
			return Ok(result);
		}

		// POST: api/authentication/login
		[HttpPost("login")]
		[AllowAnonymous]
		public IActionResult Login(SignIn model)
		{
			var result = _authService.LoginUser(model);

			return Ok(new { token = result.Result });
		}

		// GET: api/authentication/
		[HttpGet]
		[Authorize(Roles = "Admin, User")]
		public IActionResult CheckJWT()
		{
			return Ok("Correct JWT!");
		}

		// GET: api/authentication/admin
		[HttpGet("admin")]
		[Authorize(Roles = "Admin")]
		public IActionResult AdminService()
		{
			return Ok("Hi i am admin service");
		}
	}
}
