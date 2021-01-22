using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.CqrsFramework
{
    public class HandlerDispatcher : IHandlerDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public HandlerDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var methodInfo = this.GetType().GetMethod(nameof(HandleAsync), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(request.GetType(), typeof(TResponse));
            var result = methodInfo.Invoke(this, new[] {request});
            return (Task<TResponse>)result;
        }

        protected Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
        {
            var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            var middlewares = _serviceProvider.GetServices<IMiddleware<TRequest, TResponse>>();
            HandlerDelegate<TResponse> handlerDelegate = () => handler.HandleAsync(request);
            var resultDelegate = middlewares.Aggregate(handlerDelegate, (next, middleware) => () => middleware.HandleAsync(request, next));

            return resultDelegate();
        }
    }
}