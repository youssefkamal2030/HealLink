using HealLink.Application;
using HealLink.Infrastructure;
using HealLink.Presentation;
namespace HealLink.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            // Add services to the container.

            builder.Services
                .AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddApplication();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
