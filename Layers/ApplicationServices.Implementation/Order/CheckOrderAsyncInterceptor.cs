using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation.Order
{
    public class CheckOrderAsyncInterceptor : AsyncInterceptorBase
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckOrderAsyncInterceptor(IDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            var attribute = invocation.MethodInvocationTarget.GetCustomAttribute<CheckOrderAttribute>();
            if (attribute != null)
            {
                await CheckOrderAsync((int)invocation.Arguments[0]);
            }

            await proceed(invocation, proceedInfo);
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var attribute = invocation.MethodInvocationTarget.GetCustomAttribute<CheckOrderAttribute>();
            if (attribute != null)
            {
                await CheckOrderAsync((int) invocation.Arguments[0]);
            }

            return await proceed(invocation, proceedInfo);
        }

        private async Task CheckOrderAsync(int id)
        {
            var count = await _dbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == id);
            if (count != 1) throw new Exception("Order not found");
        }
    }
}
