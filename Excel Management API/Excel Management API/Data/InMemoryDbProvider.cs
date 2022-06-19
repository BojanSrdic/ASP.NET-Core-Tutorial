using Excel_Management_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excel_Management_API.Data
{
	public class InMemoryDbProvider
	{
		public static List<User> SeedData()
		{
			List<User> output = new()
			{
				new() { Id = 1, Username = "Toni", Email = "Toni@gmail.com" },
				new() { Id = 2, Username = "Nikola", Email = "Nikola@gmail.com" },
				new() { Id = 3, Username = "Dusan", Email = "Dusan@gmail.com" },
				new() { Id = 4, Username = "Vuk", Email = "Vuk@gmail.com" },
				new() { Id = 5, Username = "Relja", Email = "Relja@gmail.com" },
				new() { Id = 6, Username = "Stefan", Email = "Stefan@gmail.com" },
				new() { Id = 7, Username = "Vuk", Email = "Vuk@gmail.com" },
				new() { Id = 8, Username = "Pedja", Email = "Pedja@gmail.com" }
			};

			return output;
		}
	}
}
