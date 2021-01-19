using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQ.CqrsFramework;
using Handlers.UseCases.Product.Commands.DeleteProduct;
using Infrastructure.Interfaces;

namespace CQ.UseCases.Product.Commands.DeleteAllProducts
{
    public class DeleteAllProductsCommandHandler : ICommandHandler<DeleteAllProductsCommand>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IDbContext _dbContext;

        public DeleteAllProductsCommandHandler(ICommandDispatcher commandDispatcher, IDbContext dbContext)
        {
            _commandDispatcher = commandDispatcher;
            _dbContext = dbContext;
        }

        public async Task HandleAsync(DeleteAllProductsCommand request)
        {
            using (var transaction = _dbContext.BeginTransaction())
            {
                foreach (var id in request.Dto.Ids)
                {
                    await _commandDispatcher.SendAsync(new DeleteProductCommand { Id = id });
                }

                await transaction.CommitAsync();
            }
            
        }
    }
}
