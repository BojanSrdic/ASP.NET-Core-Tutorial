using MediatR;
using MediatR_CRUD.Models;

namespace MediatR_CRUD.Queries
{
	public record GetProductByIdQuery(int Id) : IRequest<Product>;

}
