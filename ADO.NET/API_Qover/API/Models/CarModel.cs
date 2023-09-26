using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	public class CarModel
	{
		[Key]
		public int Car_id { get; set; }
		public string Model_of_the_car { get; set; }
		public int Purchase_price { get; set; }
	}
}
