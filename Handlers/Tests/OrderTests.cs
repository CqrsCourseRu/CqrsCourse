using System;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Autofac.Extensions.DependencyInjection;
using DataAccess.MsSql;
using Entities;
using Handlers.CqrsFramework;
using Handlers.UseCases.Order.Commands.UpdateOrder;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using Xunit;

namespace Tests
{
    public class OrderTests
    {
        [Fact]
        public async Task CheckOrderTest()
        {
            var services = new ServiceCollection();
            DIHelper.ConfigureServices(services);

            services.AddDbContext<IReadOnlyDbContext, AppDbContext>(builder =>
                builder.UseInMemoryDatabase("Test"));
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseInMemoryDatabase("Test"));

            var factory = new AutofacServiceProviderFactory();
            var builder = factory.CreateBuilder(services);
            var sp = factory.CreateServiceProvider(builder);

            var dbContext = sp.GetRequiredService<IDbContext>();
            dbContext.Orders.AddRange(new[]
            {
                new Order { Id = 1, UserEmail = "test@test.test"},
                new Order { Id = 2, UserEmail = "other"}
            });
            await dbContext.SaveChangesAsync();

            var dispatcher = sp.GetRequiredService<IHandlerDispatcher>();

            var get1Result = await dispatcher.SendAsync(new GetOrderByIdQuery {Id = 1});
            Func<Task<OrderDto>> get2Delegate = () => dispatcher.SendAsync(new GetOrderByIdQuery {Id = 2});

            Assert.Equal(1, get1Result.Id);
            await Assert.ThrowsAsync<Exception>(get2Delegate);

            var update1Result = await dispatcher.SendAsync(new UpdateOrderCommand { Id = 1, Dto = new ChangeOrderDto()});
            Func<Task<Unit>> update2Delegate = () => dispatcher.SendAsync(new UpdateOrderCommand() { Id = 2, Dto = new ChangeOrderDto()});

            await Assert.ThrowsAsync<Exception>(update2Delegate);

        }
    }
}
