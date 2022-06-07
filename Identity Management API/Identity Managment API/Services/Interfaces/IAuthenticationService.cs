using Identity_Managment_API.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Managment_API.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task RegisterUser(SignUp model);
		Task<string> LoginUser(SignIn model);
	}
}
