using Identity_Managment_API.DataTransferObjects;
using Identity_Managment_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Managment_API.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private IConfiguration _configuration;

		public AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task RegisterUser(SignUp model)
		{

			// ToDo: Check if password is equal
			if (model.Password != model.ConfirmPassword)
				throw new Exception("Password doesn't match");
			

			// ToDo: Map Data transfer object to object in the database
			var mappedUser = new IdentityUser()
			{
				Email = model.Email,
				UserName = model.UserName
			};

			// ToDo: Save changes
			await _userManager.CreateAsync(mappedUser, model.Password);

			// ToDo: Set users role
			var user = await _userManager.FindByNameAsync(model.UserName);
			await _userManager.AddToRoleAsync(user, "User");


			// ToDo: Send Confirmation Email

		}
		public async Task<string> LoginUser(SignIn model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			var role = await _userManager.GetRolesAsync(user);

			// ToDo: Check password
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				IdentityOptions options = new IdentityOptions();

				// ToDo: Set Claims
				var claims = new Claim[]
				{
					new Claim("UserID", user.Id.ToString()),
					new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
				};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:JWT_Secret"]));

				// ToDo: Generate JSON Web Token
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddDays(30),
					SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
				};

				var tokenHandler = new JwtSecurityTokenHandler();
				var secutiryToken = tokenHandler.CreateToken(tokenDescriptor);
				var token = tokenHandler.WriteToken(secutiryToken);

				return token;
			}

			throw new Exception("Invalid data, check email and password");
			//return "Invalid data, check email and password";
		}
	}
}