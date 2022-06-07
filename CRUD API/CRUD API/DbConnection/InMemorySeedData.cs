using CRUD_Solid.Entitys;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Solid.DbConnection
{
	public class InMemorySeedData
	{
		public static void AddTestDataInMemory(IApplicationBuilder app)
		{
			var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetService<DatabaseContext>();
			SeedData(context);
		}

		private static void SeedData(DatabaseContext context)
		{
			var customer = new[]
			{
				new UserModel { Id = 1, UserName = "Bojan", Email="", Password = "", Age =25 }
			};

			context.Users.AddRange(customer);
			context.SaveChanges();
		}
	}
}
