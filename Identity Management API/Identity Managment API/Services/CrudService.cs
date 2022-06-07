using Identity_Managment_API.DbConnection;
using Identity_Managment_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Managment_API.Services
{
	public class CrudService : ICrudService
	{
		private readonly DatabaseContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public CrudService(DatabaseContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IdentityUser GetUser(string id)
		{
			var user = _context.Users.Find(id);
			var userRole = _userManager.GetRolesAsync(user).Result.First();

			return user;
		}

		public async Task<IdentityUser> GetUserByName(string name)
		{
			var user = await _userManager.FindByNameAsync(name);
			var userRole = _userManager.GetRolesAsync(user);

			return user;
		}

		public List<IdentityUser> GetUsers()
		{
			return _context.Users.ToList();
		}

	}
}
