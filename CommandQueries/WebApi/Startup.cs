using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using AutoMapper;
using CQ.CqrsFramework;
using DataAccess.MsSql;
using Handlers.UseCases.Order.Commands.CreateOrder;
using Handlers.UseCases.Order.Commands.UpdateOrder;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddScoped<IOrderService, OrderService>();

            services.AddAutoMapper(typeof(OrderMapperProfile));
            services.AddDbContext<IReadOnlyDbContext, ReadOnlyAppDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.Scan(selector =>
                selector.FromAssemblyOf<GetOrderByIdQuery>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            services.Scan(selector =>
                selector.FromAssemblyOf<UpdateOrderCommand>()
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
