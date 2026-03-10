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

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60); // Session timeout.
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor for accessing HttpContext and dealing with cookies in services
            builder.Services.AddScoped<IApiAccountsService, ApiAccountsService>();
            builder.Services.AddScoped<IApiCartsService, ApiCartsService>();
            builder.Services.AddScoped<IApiCategoriesService, ApiCategoriesService>();
            builder.Services.AddScoped<IApiOrdersService, ApiOrdersService>();
            builder.Services.AddScoped<IApiProductsService, ApiProductsService>();
            builder.Services.AddScoped<IApiUsersService, ApiUsersService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
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
