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
                var tasks = request.Dto.Ids.Select(x =>
                {
                    var command  = new DeleteProductCommand {Id = x};
                    return _handlerDispatcher.SendAsync<DeleteProductCommand, Task>(command);
                });

                await Task.WhenAll(tasks);

                await transaction.CommitAsync();
            }    
        }
    }
}
