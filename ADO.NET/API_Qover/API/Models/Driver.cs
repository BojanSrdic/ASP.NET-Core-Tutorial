using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	public class Driver
	{
		[Key]
		public int Driver_id { get; set; }
		public int Age_of_the_driver { get; set; }
	}
}
