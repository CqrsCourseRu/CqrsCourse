using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using AutoMapper;
using Castle.DynamicProxy;
using DataAccess.MsSql;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Implementation;
using Layers.ApplicationServices.Implementation.Order;
using Layers.ApplicationServices.Interfaces;
using Layers.ApplicationServices.Interfaces.Product;
using Layers.WebApi;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
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

            //services.AddScoped<IOrderService>(serviceProvider =>
            //{
            //    var service = serviceProvider.GetRequiredService<OrderService>();
            //    var interceptor = serviceProvider.GetRequiredService<CheckOrderAsyncInterceptor>();
            //    var proxyGenerator = new ProxyGenerator();
            //    var proxy =
            //        proxyGenerator.CreateInterfaceProxyWithTargetInterface<IOrderService>(service, interceptor);
            //    return proxy;
            //} );
            //services.AddScoped<OrderService>();
            //services.AddScoped<ReadOnlyOrderService>();
            //services.AddScoped<CheckOrderAsyncInterceptor>();
            //services.AddScoped<IReadOnlyOrderService>(serviceProvider =>
            //{
            //    var service = serviceProvider.GetRequiredService<ReadOnlyOrderService>();
            //    var interceptor = serviceProvider.GetRequiredService<CheckOrderAsyncInterceptor>();
            //    var proxyGenerator = new ProxyGenerator();
            //    var proxy =
            //        proxyGenerator.CreateInterfaceProxyWithTargetInterface<IReadOnlyOrderService>(service, interceptor);
            //    return proxy;
            //});

            services.AddScoped<IOrderService, OrderService>();
            //services.Decorate<IOrderService, OrderServiceDecorator>();
            services.AddScoped<IReadOnlyOrderService, ReadOnlyOrderService>();
            //services.Decorate<IReadOnlyOrderService, ReadOnlyOrderServiceDecorator>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReadOnlyProductService, ReadOnlyProductService>();

            services.AddScoped<IStatisticService, StatisticService>();

            services.AddAutoMapper(typeof(MapperProfile));
            if (_environment.IsEnvironment("Testing"))
            {
                services.AddDbContext<IDbContext, AppDbContext>(builder =>
                    builder.UseInMemoryDatabase("Test"));
                services.AddDbContext<IReadOnlyDbContext, AppDbContext>(builder =>
                    builder.UseInMemoryDatabase("Test"));
            }
            else
            {
                services.AddDbContext<IDbContext, AppDbContext>(builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("Database")));
                services.AddDbContext<IReadOnlyDbContext, ReadOnlyAppDbContext>(builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("Database")));

            }
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<CheckOrderFilterAttribute>();
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
