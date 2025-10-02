using E_Commerce.API.Data;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;
using E_Commerce.API.Services;
using E_Commerce.API.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.API
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
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
            builder.Services.AddScoped<UOW>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services
                .AddAuthentication(opt => opt.DefaultAuthenticateScheme = "name")
                .AddJwtBearer("name",
                option =>
                {
                    string secretKey = "welcome to my programming world while you can convert your dreams into reality";
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
