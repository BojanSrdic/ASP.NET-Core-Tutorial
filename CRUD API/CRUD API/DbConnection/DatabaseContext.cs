using CRUD_Solid.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Solid.DbConnection
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
		{

		}

		public DbSet<UserModel> Users { get; set; }
	}
}
