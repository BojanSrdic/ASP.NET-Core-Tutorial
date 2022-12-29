using MediatR;
using MediatR_CRUD.Commands;
using MediatR_CRUD.Models;
using MediatR_CRUD.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediatR_CRUD.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ISender _sender;

		public ProductController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<ActionResult> GetProducts()
		{
			var products = await _sender.Send(new GetProductsQuery());
			return Ok(products);
		}

		[HttpPost]
		public async Task<ActionResult> AddProduct([FromBody] Product product)
		{
			var productToReturn = await _sender.Send(new AddProductCommand(product));
			return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);
		}

		[HttpGet("{id:int}", Name = "GetProductById")]
		public async Task<ActionResult> GetProductById(int id)
		{
			var product = await _sender.Send(new GetProductByIdQuery(id));
			return Ok(product);
		}
	}
}
