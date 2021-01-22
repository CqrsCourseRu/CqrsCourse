using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order
{
    public class CheckOrderMiddleware<TRequest, TResponse> : IMiddleware<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>, ICheckOrderRequest
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckOrderMiddleware(IDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next)
        {
            var count = await _dbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == request.Id);
            if (count != 1) throw new Exception("Order not found");

            return await next();
        }
    }
}
