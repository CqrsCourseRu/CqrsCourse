using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;
using Handlers.UseCases.Product.Commands.DeleteProduct;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.UseCases.Product.Commands.DeleteAllProducts
{
    public class DeleteAllProductsCommandHandler : RequestHandler<DeleteAllProductsCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IHandlerDispatcher _handlerDispatcher;

        public DeleteAllProductsCommandHandler(IDbContext dbContext, IHandlerDispatcher handlerDispatcher)
        {
            _dbContext = dbContext;
            _handlerDispatcher = handlerDispatcher;
        }

        protected override async Task HandleAsync(DeleteAllProductsCommand request)
        {
            using (var transaction = _dbContext.BeginTransaction())
            {
                foreach (var id in request.Dto.Ids)
                {
                    var command = new DeleteProductCommand { Id = id };
                    await _handlerDispatcher.SendAsync(command);
                }

                await transaction.CommitAsync();
            }    
        }
    }
}
