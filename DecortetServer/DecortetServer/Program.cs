using DecortetServer.Core.Interfaces.Repositories;
using DecortetServer.Core.Interfaces.Services;
using DecortetServer.Persistense;
using DecortetServer.Persistense.Repositories;
using Microsoft.EntityFrameworkCore;
using DecortetServer.Core.Services;
using Microsoft.Extensions.Configuration;
using DecortetServer.Infrastructure.Settings;
using DecortetServer.Infrastructure.Services;

namespace DecortetServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();

            builder.Services.Configure<CloudinarySettigns>(
               builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddSingleton<IPhotoService, CloudinaryPhotoService>();

            builder.Services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddCors(options =>
            {
                var frontendUrl = config.GetValue<string>("fontend_url");

                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
