using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Managment_API.DbConnection
{
	public class DatabaseContext : IdentityDbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
		{

		}
	}
}
