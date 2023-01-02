using DotNetCore.BusinessLogic.Services;

namespace DotNetCore.WebAppRazorPages
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            //builder.Services.AddHttpContextAccessor(); //Allow Access HttpContent from another Project like BusinessLogic
            builder.Services.AddTransient<IProductsService,ProductsService>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            //Config Routing
            app.MapRazorPages();
            //app.MapGet("/products",async context =>
            //{
            //    var jsonService = app.Services.GetService<IProductsService>();

            //    var products = await jsonService?.GetAllProductsFromJsonFileAsync()!;

            //    await context.Response.WriteAsJsonAsync(products);
            //});

            await app.RunAsync();
            
        }
    }
}