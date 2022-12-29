using MediatR;
using MediatR_CRUD.Commands;
using MediatR_CRUD.Data;
using MediatR_CRUD.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_CRUD.Handlers
{
	public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly FakeDataStore _fakeDataStore;
        public AddProductHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _fakeDataStore.AddProduct(request.Product);

            return request.Product;
        }
    }
}