using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Managment_API.Services.Interfaces
{
	public interface ICrudService
	{
		public IdentityUser GetUser(string id);
		public List<IdentityUser> GetUsers();
	}
}
