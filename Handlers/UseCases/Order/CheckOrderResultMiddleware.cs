using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.ApplicationServices.Interfaces.Exceptions;
using Handlers.CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order
{
    public class CheckOrderResultMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>, ICheckOrderResultRequest
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckOrderResultMiddleware(IDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Result<TResponse>> HandleAsync(TRequest request, HandlerDelegate<Result<TResponse>> next)
        {
            var count = await _dbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == request.Id);
            if (count != 1) return Result<TResponse>.Fail();

            return await next();
        }
    }
}
