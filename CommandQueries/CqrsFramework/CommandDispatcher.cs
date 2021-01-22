using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.CqrsFramework
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            var middlewares = _serviceProvider.GetServices<ICommandMiddleware<TCommand>>();
            CommandHandlerDelegate handlerDelegate = () => handler.HandleAsync(command);
            var resultDelegate = middlewares.Aggregate(handlerDelegate,
                (next, middleware) => () => middleware.HandleAsync(command, next));

            return resultDelegate();
        }
    }
}