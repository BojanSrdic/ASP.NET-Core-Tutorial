using MediatR;
using MediatR_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR_CRUD.Commands
{
	public record AddProductCommand(Product Product) : IRequest<Product>;

}
