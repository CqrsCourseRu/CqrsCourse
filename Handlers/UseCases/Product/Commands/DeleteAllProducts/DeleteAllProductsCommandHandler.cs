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
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContext _dbContext;

        public DeleteAllProductsCommandHandler(IServiceProvider serviceProvider, IDbContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        protected override async Task HandleAsync(DeleteAllProductsCommand request)
        {
            using (var transaction = _dbContext.BeginTransaction())
            {
                var tasks = request.Dto.Ids.Select(x =>
                {
                    var deleteHandler = _serviceProvider.GetRequiredService<IRequestHandler<DeleteProductCommand>>();
                    return deleteHandler.HandleAsync(new DeleteProductCommand {Id = x});
                });

                await Task.WhenAll(tasks);

                await transaction.CommitAsync();
            }    
        }
    }
}
