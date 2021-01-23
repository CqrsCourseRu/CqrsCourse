using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApi;
using Xunit;

namespace Tests
{
    public class OrderTests
    {
        [Fact]
        public async Task CheckOrderTest()
        {
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            var testServer = new TestServer(builder);
            var client = testServer.CreateClient();
            var dbContext = testServer.Host.Services.GetRequiredService<IDbContext>();
            dbContext.Orders.AddRange(new []
            {
                new Order { Id = 1, UserEmail = "test@test.test"},
                new Order { Id = 2, UserEmail = "other"}
            });
            await dbContext.SaveChangesAsync();

            var get1Reponse = await client.GetAsync("/orders/1");
            var get2Reponse = await client.GetAsync("/orders/2");

            var dto = new ChangeOrderDto();
            var dtoString = JsonConvert.SerializeObject(dto);
            var content = new StringContent(dtoString, Encoding.UTF8, "application/json");
            var put1Reponse = await client.PutAsync("/orders/1", content);
            var put2Reponse = await client.PutAsync("/orders/2", content);

            Assert.True(get1Reponse.IsSuccessStatusCode);
            Assert.False(get2Reponse.IsSuccessStatusCode);

            Assert.True(put1Reponse.IsSuccessStatusCode);
            Assert.False(put2Reponse.IsSuccessStatusCode);

        }
    }
}
