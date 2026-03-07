using E_Commerce.MVC.Services;

namespace E_Commerce.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient("ECommerceApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor for accessing HttpContext and dealing with cookies in services
            builder.Services.AddScoped<IApiAccountsService, ApiAccountsService>();
            builder.Services.AddScoped<IApiCartService, ApiCartService>();
            builder.Services.AddScoped<IApiCategoryService, ApiCategoryService>();
            builder.Services.AddScoped<IApiOrderService, ApiOrderService>();
            builder.Services.AddScoped<IApiProductService, ApiProductService>();
            builder.Services.AddScoped<IApiUserService, ApiUserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
