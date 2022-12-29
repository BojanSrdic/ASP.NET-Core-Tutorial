using MediatR;
using MediatR_CRUD.Models;
using System.Collections.Generic;

namespace MediatR_CRUD.Queries
{
	public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
}
