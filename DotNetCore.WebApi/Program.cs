using System.Reflection;
using DotNetCore.BusinessLogic.Services;
using Microsoft.OpenApi.Models;

namespace DotNetCore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            //config swagger as api documentation tool
            builder.Services.AddSwaggerGen(c =>
            {
                //define swagger document options
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DotNetCore.WebApi",
                    Description = "Test Asp.net Core WebApi",
                    Version = "v1.0",
                    Contact = new OpenApiContact
                    {
                        Name = "Eng.MohammedSalah",
                        Email = "Email@mail.com",
                        Url = new Uri("http://www.google.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Free Uses"
                    }

                });

                //Allow Swagger to read xml documentation file that stored comments over methods
                var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                if (baseDirectory != null)
                {
                    var xmlFilePath = Path.Combine(baseDirectory, xmlFileName);

                    c.IncludeXmlComments(xmlFilePath);
                }

            });
            builder.Services.AddSwaggerGenNewtonsoftSupport();

            //config app services inside middleware
            builder.Services.AddTransient<IProductsService, ProductsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //On Development Stage
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //On Production Stage
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}