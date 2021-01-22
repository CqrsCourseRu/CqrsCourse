using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQ.CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CQ.UseCases.Order
{
    public class CheckOrderCommandMiddleware<TRequest> : CheckOrderMiddleware<TRequest, string> 
        where TRequest : ICheckOrderRequest
    {
        public CheckOrderCommandMiddleware(IDbContext dbContext, ICurrentUserService currentUserService) : base(dbContext, currentUserService)
        {
        }
    }

    public class CheckOrderMiddleware<TRequest, TResponse> : 
            ICommandMiddleware<TRequest>, 
            IQueryMiddleware<TRequest, TResponse>
        where TRequest : ICheckOrderRequest
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckOrderMiddleware(IDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task HandleAsync(TRequest request, CommandHandlerDelegate next)
        {
            await CheckOrderAsync(request);

            await next();
        }

        private async Task CheckOrderAsync(TRequest request)
        {
            var count = await _dbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == request.Id);
            if (count != 1) throw new Exception("Order not found");
        }

        public async Task<TResponse> HandleAsync(TRequest request, QueryHandlerDelegate<TResponse> next)
        {
            await CheckOrderAsync(request);

            return await next();
        }
    }
}
